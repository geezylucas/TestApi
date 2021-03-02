using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectData.Model;
using BusinessLogic.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace BusinessLogic.Services
{
    public class AreaServiceImpl : IAreaService
    {
        private readonly TestContext _context;
        private readonly IMapper _mapper;

        public AreaServiceImpl(TestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AreaDTO>> GetAreas()
        {
            var areasEntity = await _context.Areas.FromSqlRaw("EXEC sp_select_area").ToListAsync();
            return _mapper.Map<List<AreaDTO>>(areasEntity);
        }

        public async Task<AreaDTO> GetArea(int id)
        {
            var areasEntity = await _context.Areas.FromSqlInterpolated($"EXEC sp_select_area_by_id {id}").ToListAsync();
            return _mapper.Map<AreaDTO>(areasEntity.FirstOrDefault());
        }

        public async Task<ClassBase<AreaDTO>> InsertArea(AreaDTO area)
        {
            ClassBase<AreaDTO> classBaseAreaDTO = new ClassBase<AreaDTO>();

            try
            {
                var areaEntity = await _context.Areas.FromSqlInterpolated($"EXEC sp_insert_area {area.Name}").ToListAsync();
                classBaseAreaDTO.AnyClass = _mapper.Map<AreaDTO>(areaEntity.FirstOrDefault());
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2601:
                        classBaseAreaDTO.Error = $"El registro {area.Name} ya existe en la base de datos.";
                        classBaseAreaDTO.AnyClass = null;
                        break;
                    default:
                        classBaseAreaDTO.Error = $"Error: {ex.Message}";
                        classBaseAreaDTO.AnyClass = null;
                        break;
                }
            }

            return classBaseAreaDTO;
        }

        public string EditArea(AreaDTO area)
        {
            try
            {
                _context.Database.ExecuteSqlInterpolated($"EXEC sp_update_area {area.Id}, {area.Name}");
            }
            catch (SqlException ex)
            {
                return $"Error: {ex.Message}";
            }

            return "OK";
        }

        public string RemoveArea(int id, string name)
        {
            try
            {
                _context.Database.ExecuteSqlInterpolated($"EXEC sp_delete_area {id}");
            }
            catch (SqlException ex)
            {
                return ex.Number switch
                {
                    547 => $"No se puede borrar el registro {name}, existe relación con algunas subáreas.",
                    _ => $"Error: {ex.Message}",
                };
            }

            return "OK";
        }
    }
}

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
    public class SubAreaServiceImpl : ISubAreaService
    {
        private readonly TestContext _context;
        private readonly IMapper _mapper;

        public SubAreaServiceImpl(TestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<SubAreaDTO>> GetSubAreas()
        {
            var subAreasEntity = await _context.SPSelectSubAreas.FromSqlRaw("EXEC sp_select_subareas").ToListAsync();
            return _mapper.Map<List<SubAreaDTO>>(subAreasEntity);
        }

        public async Task<SubAreaDTO> GetSubArea(int id)
        {
            var areasEntity = await _context.SPSelectSubAreas.FromSqlInterpolated($"EXEC sp_select_subarea_by_id {id}").ToListAsync();
            return _mapper.Map<SubAreaDTO>(areasEntity.FirstOrDefault());
        }

        public async Task<ClassBase<SubAreaDTO>> InsertSubArea(SubAreaDTO subArea)
        {
            ClassBase<SubAreaDTO> classBaseSubAreaDTO = new ClassBase<SubAreaDTO>();

            try
            {
                var subAraeaEntity = await _context.SubAreas.FromSqlInterpolated($"EXEC sp_insert_subarea {subArea.Name}, {subArea.AreaId}").ToListAsync();
                classBaseSubAreaDTO.AnyClass = _mapper.Map<SubAreaDTO>(subAraeaEntity.FirstOrDefault());
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2601:
                        classBaseSubAreaDTO.Error = $"El registro {subArea.Name} ya existe en la base de datos.";
                        classBaseSubAreaDTO.AnyClass = null;
                        break;
                    default:
                        classBaseSubAreaDTO.Error = $"Error: {ex.Message}";
                        classBaseSubAreaDTO.AnyClass = null;
                        break;
                }
            }

            return classBaseSubAreaDTO;
        }

        public string EditSubArea(SubAreaDTO subArea)
        {
            try
            {
                _context.Database.ExecuteSqlInterpolated($"EXEC sp_update_subarea {subArea.Id}, {subArea.Name}");
            }
            catch (SqlException ex)
            {
                return $"Error: {ex.Message}";
            }

            return "OK";
        }

        public string RemoveSubArea(int id, string name)
        {
            try
            {
                _context.Database.ExecuteSqlInterpolated($"EXEC sp_delete_subarea {id}");
            }
            catch (SqlException ex)
            {
                return ex.Number switch
                {
                    547 => $"No se puede borrar el registro {name}, existe relación con algunos usuarios.",
                    _ => $"Error: {ex.Message}",
                };
            }

            return "OK";
        }
    }
}

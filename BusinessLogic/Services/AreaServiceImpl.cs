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

        /// <summary>
        /// Method to get all rows from Area Table through stored procedure call sp_select_area
        /// </summary>
        /// <returns></returns>
        public async Task<List<AreaDTO>> GetAreas()
        {
            var areasEntity = await _context.Areas.FromSqlRaw("EXEC sp_select_area").ToListAsync();
            return _mapper.Map<List<AreaDTO>>(areasEntity);
        }

        /// <summary>
        /// Method to get row by id from Area Table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AreaDTO> GetArea(int id)
        {
            var areasEntity = await _context.Areas.FromSqlInterpolated($"EXEC sp_select_area_by_id {id}").ToListAsync();
            return _mapper.Map<AreaDTO>(areasEntity.FirstOrDefault());
        }

        /// <summary>
        /// Method to insert row from Area Table
        /// </summary>
        /// <returns></returns>
        public async Task<ClassBase<AreaDTO>> InsertArea(AreaDTO area)
        {
            ClassBase<AreaDTO> classBaseAreaDTO = new ClassBase<AreaDTO>();

            try
            {
                var areasEntity = await _context.Areas.FromSqlInterpolated($"EXEC sp_insert_area {area.Name}").ToListAsync();
                classBaseAreaDTO.AnyClass = _mapper.Map<AreaDTO>(areasEntity.FirstOrDefault());
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
                        break;
                }
            }

            return classBaseAreaDTO;
        }

        /// <summary>
        /// Method to delete row from Area Table
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string RemoveArea(int id, string name)
        {
            try
            {
                _context.Database.ExecuteSqlInterpolated($"EXEC sp_delete_area {id}");
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 547:
                        return $"No se puede borrar el registro {name}, existe relación con algunas subáreas.";
                    default:
                        break;
                }
            }

            return "OK";
        }
    }
}

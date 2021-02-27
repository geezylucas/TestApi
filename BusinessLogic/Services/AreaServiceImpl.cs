using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectData.Model;
using BusinessLogic.DTOs;
using Microsoft.EntityFrameworkCore;

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
            var areasEntity = await _context.Areas.FromSqlInterpolated($"EXEC sp_select_area").ToListAsync();
            var areasDTO = _mapper.Map<List<AreaDTO>>(areasEntity);

            return areasDTO;
        }
    }
}

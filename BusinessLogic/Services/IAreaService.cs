using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services
{
    /* Interface to provide methods necessary to anyone */
    public interface IAreaService
    {
        public Task<List<AreaDTO>> GetAreas();
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using ConnectData.DTOs;

namespace ConnectData.Services
{
    /* Interface to provide methods necessary to anyone */
    public interface IAreaService
    {
        public Task<List<AreaDTO>> GetAreas();
    }
}

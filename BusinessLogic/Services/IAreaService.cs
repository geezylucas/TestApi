using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services
{
    /* Interface to provide methods necessary to anyone */
    public interface IAreaService
    {
        public Task<List<AreaDTO>> GetAreas();

        public Task<AreaDTO> GetArea(int id);

        public Task<ClassBase<AreaDTO>> InsertArea(AreaDTO area);

        public Task<string> EditArea(AreaDTO area);

        public Task<string> RemoveArea(int id, string name);
    }
}

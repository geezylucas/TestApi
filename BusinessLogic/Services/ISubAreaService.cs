using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services
{
    /* Interface to provide methods necessary to anyone */
    public interface ISubAreaService
    {
        public Task<List<SubAreaDTO>> GetSubAreas();

        public Task<SubAreaDTO> GetSubArea(int id);

        public Task<ClassBase<SubAreaDTO>> InsertSubArea(SubAreaDTO subArea);

        public Task<string> EditSubArea(SubAreaDTO subArea);

        public Task<string> RemoveSubArea(int id, string name);
    }
}

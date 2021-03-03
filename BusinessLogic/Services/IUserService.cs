using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogic.DTOs;

namespace BusinessLogic.Services
{
    /* Interface to provide methods necessary to anyone */
    public interface IUserService
    {
        public Task<List<UserDTO>> GetUsers();

        public Task<UserDTO> GetUser(int id);

        public Task<string> Authentication(UserDTO user);

        public Task<ClassBase<UserDTO>> InsertUser(UserDTO user);

        public string EditUser(UserDTO user);

        public string RemoveUser(int id);
    }
}

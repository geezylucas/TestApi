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
    public class UserServiceImpl : IUserService
    {
        private readonly TestContext _context;
        private readonly IMapper _mapper;

        public UserServiceImpl(TestContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            var usersEntity = await _context.SPSelectUsers.FromSqlRaw("EXEC sp_select_users").ToListAsync();
            return _mapper.Map<List<UserDTO>>(usersEntity);
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var userEntity = await _context.SPSelectUsers.FromSqlInterpolated($"EXEC sp_select_user_by_id {id}").ToListAsync();
            return _mapper.Map<UserDTO>(userEntity.FirstOrDefault());
        }

        public async Task<ClassBase<UserDTO>> InsertUser(UserDTO user)
        {
            ClassBase<UserDTO> classBaseUserDTO = new ClassBase<UserDTO>();

            try
            {
                var userEntity = await _context.Users.FromSqlInterpolated($"EXEC sp_insert_user {user.Email}, {user.Password}, {user.Name}, {user.Lastname}, {user.SecondLastname}, {user.SubAreaId}").ToListAsync();
                classBaseUserDTO.AnyClass = _mapper.Map<UserDTO>(userEntity.FirstOrDefault());
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 2601:
                        classBaseUserDTO.Error = $"El registro {user.Email} ya existe en la base de datos.";
                        classBaseUserDTO.AnyClass = null;
                        break;
                    default:
                        classBaseUserDTO.Error = $"Error: {ex.Message}";
                        classBaseUserDTO.AnyClass = null;
                        break;
                }
            }

            return classBaseUserDTO;
        }

        public string EditUser(UserDTO user)
        {
            try
            {
                _context.Database.ExecuteSqlInterpolated($"EXEC sp_update_user {user.Id}, {user.Password}, {user.Name}, {user.Lastname}, {user.SecondLastname}, {user.SubAreaId}");
            }
            catch (SqlException ex)
            {
                return $"Error: {ex.Message}";
            }

            return "OK";
        }

        public string RemoveUser(int id)
        {
            try
            {
                _context.Database.ExecuteSqlInterpolated($"EXEC sp_delete_user {id}");
            }
            catch (SqlException ex)
            {
                return $"Error: {ex.Message}";
            }

            return "OK";
        }
    }
}

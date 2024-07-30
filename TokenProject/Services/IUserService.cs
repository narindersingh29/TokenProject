using TokenProject.dtos;

namespace TokenProject.Services
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserDto userDto);
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
        Task<bool> UpdateUser(UserDto userDto);
        Task<bool> DeleteUser(int id);
    }
}

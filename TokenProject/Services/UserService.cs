using AutoMapper;
using TokenProject.dtos;
using TokenProject.Entities;
using TokenProject.IRepositories;


namespace TokenProject.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEncryptionService _encryptionService;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IEncryptionService encryptionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _encryptionService = encryptionService;
        }
        public async Task<bool> CreateUser(UserDto userDto)
        {
            var dtos = _mapper.Map<User>(userDto);
            dtos.Password = _encryptionService.Encrypt(userDto.Password);
            await _unitOfWork.Users.Add(dtos);
            var result = _unitOfWork.Save();
            if (result > 0)
                return true;
            else
                return false;
        }
        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var entities = await _unitOfWork.Users.GetAll();
            var dtos = _mapper.Map<IEnumerable<UserDto>>(entities);
            return dtos;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var entities = await _unitOfWork.Users.GetById(id);
            var dtos = _mapper.Map<UserDto>(entities);
            return dtos;
        }

        public async Task<bool> UpdateUser(UserDto userDto)
        {
            var dtos = _mapper.Map<User>(userDto);
            _unitOfWork.Users.Update(dtos);
            var result = _unitOfWork.Save();
            if (result > 0)
                return true;
            else
                return false;
        }
        public async Task<bool> DeleteUser(int id)
        {
            var users = await _unitOfWork.Users.GetById(id);
            if (users != null)
            {
                _unitOfWork.Users.Delete(users);
                var result = _unitOfWork.Save();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}

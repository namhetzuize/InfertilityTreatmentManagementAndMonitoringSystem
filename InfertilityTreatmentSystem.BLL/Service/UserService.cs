using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.BLL.Service
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Login (string username, string password)
        {
            return await _unitOfWork.UserRepository.LoginAsync(username, password);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.UserRepository.GetByIdAsync(id);
        }

        public async Task CreateUserAsync(User user)
        {
            _unitOfWork.UserRepository.PrepareCreate(user);
            await _unitOfWork.UserRepository.SaveAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _unitOfWork.UserRepository.PrepareUpdate(user);
            await _unitOfWork.UserRepository.SaveAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _unitOfWork.UserRepository.PrepareRemove(user);
            await _unitOfWork.UserRepository.SaveAsync();
        }
    }
}

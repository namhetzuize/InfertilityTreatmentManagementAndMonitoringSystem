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

        public async Task<User> GetUserByIdAsync(Guid id)
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

        public async Task UpdateUserByIdAsync(Guid userId, User updatedUser)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            // Update the user properties
            user.UserName = updatedUser.UserName;
            user.Password = updatedUser.Password;
            user.FullName = updatedUser.FullName;
            user.Age = updatedUser.Age;
            user.PhoneNumber = updatedUser.PhoneNumber;
            user.Role = updatedUser.Role;
            user.IsActive = updatedUser.IsActive;

            _unitOfWork.UserRepository.PrepareUpdate(user);
            await _unitOfWork.UserRepository.SaveAsync();
        }

        // Delete user by UserId
        public async Task DeleteUserByIdAsync(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            _unitOfWork.UserRepository.PrepareRemove(user);
            await _unitOfWork.UserRepository.SaveAsync();
        }
    }
}

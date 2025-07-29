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

        // Adding GetUserByUserNameAsync method to check if user exists by UserName
        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return await _unitOfWork.UserRepository.GetUserByUserNameAsync(username);
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

            _unitOfWork.UserRepository.PrepareUpdate(user);
            await _unitOfWork.UserRepository.SaveAsync();
        }

        // Delete user by UserId
        // InfertilityTreatmentSystem.BLL.Service/UserService.cs

        public async Task DeleteUserByIdAsync(Guid userId)
        {
            // 1) Remove Blogs
            var allBlogs = await _unitOfWork.BlogRepository.GetAllAsync();
            foreach (var blog in allBlogs.Where(b => b.UserId == userId))
            {
                _unitOfWork.BlogRepository.PrepareRemove(blog);
            }

            // 2) Remove Appointments where user is customer or doctor
            var allAppointments = await _unitOfWork.AppointmentRepository.GetAllAsync();
            foreach (var appt in allAppointments
                .Where(a => a.CustomerId == userId || a.DoctorId == userId))
            {
                _unitOfWork.AppointmentRepository.PrepareRemove(appt);
            }

            // 3) (If you have other child entities, e.g., PatientRequests, MedicalRecords, Schedules, 
            //    repeat the same pattern: fetch, PrepareRemove)

            // 4) Finally remove the User
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");
            _unitOfWork.UserRepository.PrepareRemove(user);

            // 5) Flush once (this will delete *all* prepared entities in one transaction)
            await _unitOfWork.UserRepository.SaveAsync();
        }

<<<<<<< HEAD

=======
>>>>>>> 40b33bbf41feaa2ca5050cd5fe29bac736328c65
    }
}

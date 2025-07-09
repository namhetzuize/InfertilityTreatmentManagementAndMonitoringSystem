using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.Data.Base;
using Microsoft.EntityFrameworkCore;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class Userrepository : GenericRepository<User>
    {
        public Userrepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }

        public async Task<User> LoginAsync(string username, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}

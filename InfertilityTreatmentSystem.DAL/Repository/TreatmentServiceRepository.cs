using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class TreatmentServiceRepository : GenericRepository<TreatmentService>
    {
        public TreatmentServiceRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }

        // Method to get TreatmentService by ServiceName using UnitOfWork pattern
        public async Task<TreatmentService> GetServiceByServiceNameAsync(string serviceName)
        {
            return await _context.TreatmentServices
                .Where(ts => ts.ServiceName.ToLower() == serviceName.ToLower())
                .FirstOrDefaultAsync();
        }
    }
}

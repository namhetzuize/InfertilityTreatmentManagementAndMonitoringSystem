using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;
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

        public async Task<PagingResponse<TreatmentService>> GetPagedTreatmentServicesAsync(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Set<TreatmentService>()
                .Include(ts => ts.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(ts => ts.ServiceName.ToLower().Contains(searchTerm.ToLower()) ||
                                           ts.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            int count = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PagingResponse<TreatmentService>
            {
                List = await query.ToListAsync(),
                TotalPages = totalPages,
                PageIndex = pageIndex
            };
        }
    }
}

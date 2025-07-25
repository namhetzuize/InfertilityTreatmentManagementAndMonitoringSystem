using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;
using InfertilityTreatmentSystem.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class PatientRequestRepository : GenericRepository<PatientRequest>
    {
        public PatientRequestRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }

        public async Task<PagingResponse<PatientRequest>> GetPagedPatientRequestsAsync(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Set<PatientRequest>()
                .Include(p => p.Customer)
                .Include(p => p.Doctor)
                .Include(p => p.Service)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Customer.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                                          p.Doctor.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                                          p.Service.ServiceName.ToLower().Contains(searchTerm.ToLower()));
            }

            int count = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PagingResponse<PatientRequest>
            {
                List = await query.ToListAsync(),
                TotalPages = totalPages,
                PageIndex = pageIndex
            };
        }
    }
}

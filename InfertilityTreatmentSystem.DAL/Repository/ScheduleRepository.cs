using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;
using InfertilityTreatmentSystem.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class ScheduleRepository: GenericRepository<Schedule>
    {
        public ScheduleRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }

        public async Task<PagingResponse<Schedule>> GetPagedSchedulesAsync(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Set<Schedule>()
                .Include(s => s.Customer)
                .Include(s => s.Doctor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(s => s.Customer.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                                          s.Doctor.FullName.ToLower().Contains(searchTerm.ToLower()));
            }

            int count = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PagingResponse<Schedule>
            {
                List = await query.ToListAsync(),
                TotalPages = totalPages,
                PageIndex = pageIndex
            };
        }
    }
}

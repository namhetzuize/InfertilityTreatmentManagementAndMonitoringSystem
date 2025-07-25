using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;
using InfertilityTreatmentSystem.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class FeedbackRepository : GenericRepository<Feedback>
    {
        public FeedbackRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }

        public async Task<PagingResponse<Feedback>> GetPagedFeedbacksAsync(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Set<Feedback>()
                .Include(f => f.Customer)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(f => f.Customer.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                                          f.Comment.ToLower().Contains(searchTerm.ToLower()));
            }

            int count = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PagingResponse<Feedback>
            {
                List = await query.ToListAsync(),
                TotalPages = totalPages,
                PageIndex = pageIndex
            };
        }
    }
}

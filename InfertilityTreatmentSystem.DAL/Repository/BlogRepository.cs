using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;
using InfertilityTreatmentSystem.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class BlogRepository : GenericRepository<Blog>
    {
        public BlogRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }

        public async Task<PagingResponse<Blog>> GetPagedBlogsAsync(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Set<Blog>()
                .Include(b => b.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Title.ToLower().Contains(searchTerm.ToLower()) ||
                                          b.Content.ToLower().Contains(searchTerm.ToLower()));
            }

            int count = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PagingResponse<Blog>
            {
                List = await query.ToListAsync(),
                TotalPages = totalPages,
                PageIndex = pageIndex
            };
        }
    }
}

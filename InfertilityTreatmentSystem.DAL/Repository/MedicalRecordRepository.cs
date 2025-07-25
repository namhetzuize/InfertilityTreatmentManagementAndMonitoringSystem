using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;
using InfertilityTreatmentSystem.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class MedicalRecordRepository : GenericRepository<MedicalRecord>
    {
        public MedicalRecordRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }

        public async Task<PagingResponse<MedicalRecord>> GetPagedMedicalRecordsAsync(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Set<MedicalRecord>()
                .Include(m => m.Customer)
                .Include(m => m.Doctor)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(m => m.Customer.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                                          m.Doctor.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                                          m.Prescription.ToLower().Contains(searchTerm.ToLower()));
            }

            int count = await query.CountAsync();
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return new PagingResponse<MedicalRecord>
            {
                List = await query.ToListAsync(),
                TotalPages = totalPages,
                PageIndex = pageIndex
            };
        }
    }
}

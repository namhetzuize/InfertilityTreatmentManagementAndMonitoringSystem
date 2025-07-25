using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;
using InfertilityTreatmentSystem.Data.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

public class AppointmentRepository : GenericRepository<Appointment>
{
    public AppointmentRepository(InfertilityTreatmentDBContext context) : base(context)
    {
    }
    public async Task<PagingResponse<Appointment>> GetPagedAppointmentsAsync(string searchTerm, int pageIndex = 1, int pageSize = 10)
    {
        var query = _context.Set<Appointment>()
            .Include(a => a.Customer)
            .Include(a => a.Doctor)
            .Include(a => a.Service)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(a => a.Customer.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                                      a.Doctor.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                                      a.Service.ServiceName.ToLower().Contains(searchTerm.ToLower()));
        }

        int count = await query.CountAsync();
        int totalPages = (int)Math.Ceiling(count / (double)pageSize);

        query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        return new PagingResponse<Appointment>
        {
            List = await query.ToListAsync(),
            TotalPages = totalPages,
            PageIndex = pageIndex
        };
    }
}

using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace InfertilityTreatmentSystem.Pages
{

    public class HomeModel : PageModel
    {
        private readonly InfertilityTreatmentDBContext _context;

        public HomeModel(InfertilityTreatmentDBContext context)
        {
            _context = context;
        }
        public List<TreatmentService> TreatmentServices { get; set; } = new List<TreatmentService>();
        public List<Blog> Blogs { get; set; } = new List<Blog>();

        public async Task OnGetAsync()
        {
            TreatmentServices = await _context.TreatmentServices.Take(3).ToListAsync();
            Blogs = await _context.Blogs.OrderByDescending(b => b.CreatedDate).Take(2).ToListAsync();
        }
    }
}

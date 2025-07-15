using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.Pages.UserPage
{
    public class IndexModel : PageModel
    {
        private readonly InfertilityTreatmentSystem.DAL.Models.InfertilityTreatmentDBContext _context;

        public IndexModel(InfertilityTreatmentSystem.DAL.Models.InfertilityTreatmentDBContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            User = await _context.Users.ToListAsync();
        }
    }
}

using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.UserPage
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;

        public IndexModel(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>All users to display in the table</summary>
        public List<User> Users { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToPage("/Error");
            }
            // pull from your service, not directly from DbContext
            Users = await _userService.GetAllUsersAsync();
            return Page();
        }
    }
}

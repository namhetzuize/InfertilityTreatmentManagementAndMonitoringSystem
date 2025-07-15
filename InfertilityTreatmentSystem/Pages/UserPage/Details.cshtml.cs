using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.UserPage
{
    public class DetailsModel : PageModel
    {
        private readonly UserService _userService;

        public DetailsModel(UserService userService)
        {
            _userService = userService;
        }

        // Renamed the model property to 'user' (lowercase)
        public User user { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid userId)
        {
            // Fetch the user by ID
            user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

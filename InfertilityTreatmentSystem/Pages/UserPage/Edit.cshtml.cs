using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.UserPage
{
    public class EditModel : PageModel
    {
        private readonly UserService _userService;

        public EditModel(UserService userService)
        {
            _userService = userService;
        }

        // Route-bound userId
        [BindProperty(SupportsGet = true)]
        public Guid UserId { get; set; }

        // The edited user
        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid userId)
        {
            // load into User
            var u = await _userService.GetUserByIdAsync(userId);
            if (u == null) return NotFound();

            User = u;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                // call the UpdateUserByIdAsync helper
                await _userService.UpdateUserByIdAsync(UserId, User);
                // redirect back to the Details page of that user
                return RedirectToPage("./Details", new { userId = UserId });
            }
            catch (Exception ex)
            {
                // optionally log ex
                ModelState.AddModelError(string.Empty, ex.Message);
                return Page();
            }
        }
    }
}

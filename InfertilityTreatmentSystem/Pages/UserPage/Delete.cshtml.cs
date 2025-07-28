using InfertilityTreatmentSystem.BLL.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.UserPage
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly UserService _userService;

        public DeleteModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid userId)
        {
            // Just render the confirmation UI; no need to load full user details
            UserId = userId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // call your service method
            await _userService.DeleteUserByIdAsync(UserId);
            return RedirectToPage("./Index");
        }
    }
}

using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace InfertilityTreatmentSystem.Pages.UserPage
{
    public class EditModel : PageModel
    {
        private readonly UserService _userService;

        public EditModel(UserService userService)
        {
            _userService = userService;
        }

        // This comes from the route
        [BindProperty(SupportsGet = true)]
        public Guid UserId { get; set; }

        // This is your DAL user, bound from the form
        [BindProperty]
        public User User { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid userId)
        {
            var u = await _userService.GetUserByIdAsync(userId);
            if (u == null) return NotFound();

            // use HttpContext.User to get the current principal’s claims
            var myId = HttpContext.User.FindFirstValue("UserId");
            var myRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            // only Admin or the user themselves may view
            if (myRole != "Admin" && myId != userId.ToString())
                return Forbid();

            User = u;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var existing = await _userService.GetUserByIdAsync(UserId);
            if (existing == null) return NotFound();

            var myId = HttpContext.User.FindFirstValue("UserId");
            var myRole = HttpContext.User.FindFirstValue(ClaimTypes.Role);

            // only Admin or the user themselves may submit
            if (myRole != "Admin" && myId != UserId.ToString())
                return Forbid();

            // always‐editable
            existing.FullName = User.FullName;
            existing.UserName = User.UserName;
            existing.Password = User.Password;
            existing.PhoneNumber = User.PhoneNumber;
            existing.Age = User.Age;

            // Admins get to flip Role & IsActive
            if (myRole == "Admin")
            {
                existing.Role = User.Role;
                existing.IsActive = User.IsActive;
            }

            await _userService.UpdateUserAsync(existing);
            return RedirectToPage("./Details", new { userId = UserId });
        }
    }
}

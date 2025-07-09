using InfertilityTreatmentSystem.BLL.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace InfertilityTreatmentSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;

        public IndexModel(UserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public string UserName { get; set; }  // Match with entity model

        [BindProperty(Name = "Password")]  // Match with entity's password property
        public string Password { get; set; }

        public IActionResult OnGet()
        {
            // Redirect if the user is already logged in
            if (HttpContext.Session.GetString("UserName") != null)
            {
                // Redirect to Blog Page if user is logged in
                return RedirectToPage("/BlogPage/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Make sure we are matching the correct property names for login
            var user = await _userService.Login(UserName, Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }

            // Store UserName and UserId in session
            HttpContext.Session.SetString("UserName", user.UserName ?? "");
            HttpContext.Session.SetString("UserId", user.UserId.ToString());

            // Redirect to BlogPage/Index after successful login
            return RedirectToPage("/BlogPage/Index");
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Clear(); // Clear session on logout
            return RedirectToPage("/Index");
        }
    }
}

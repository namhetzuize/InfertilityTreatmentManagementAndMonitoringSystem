using InfertilityTreatmentSystem.BLL.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace InfertilityTreatmentSystem.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _userService;

        public LoginModel(UserService userService)
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Home");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userService.Login(UserName, Password);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return Page();
            }


            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim("UserId", user.UserId.ToString()),
        new Claim(ClaimTypes.Role, user.Role),
        new Claim("FullName", user.FullName),
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            if (user.Role == "Doctor")
                return RedirectToPage("/Doctor");
            else if (user.Role == "Admin")
                return RedirectToPage("/AdminDashboard"); // ví dụ khác
            else
                return RedirectToPage("/Home");

        }

        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear(); // có thể giữ nếu bạn dùng session cho mục đích khác
            return RedirectToPage("/Home");
        }
    }
}

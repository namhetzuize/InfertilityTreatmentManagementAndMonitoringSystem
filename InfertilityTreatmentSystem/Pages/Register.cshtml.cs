using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public User NewUser { get; set; }

        [BindProperty]
        public string ConfirmPassword { get; set; }  // To handle password confirmation

        public RegisterModel(UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
            // Display the empty form when the page loads
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Check if the passwords match
                if (NewUser.Password != ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Passwords do not match.");
                    return Page();
                }

                // Check if the user already exists by username
                var existingUser = await _userService.GetUserByUserNameAsync(NewUser.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "Username already exists.");
                    return Page();
                }

                // Set default values for fields not included in the form
                NewUser.UserId = Guid.NewGuid(); // Generate a new UserId
                NewUser.Role = "Customer"; // Default role set to Customer
                NewUser.IsActive = false; // Default to inactive if not checked
                NewUser.Age = null; // No age input in the form
                NewUser.PhoneNumber = null; // No phone number input in the form

                // Create the new user
                await _userService.CreateUserAsync(NewUser);

                // Redirect to the login page after successful registration
                return RedirectToPage("./Login");
            }

            return Page();
        }
    }
}

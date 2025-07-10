using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace InfertilityTreatmentSystem.Pages.TreatmentServicePage
{
    public class CreateModel : PageModel
    {
        private readonly TreatmentServiceService _treatmentServiceService;
        private readonly UserService _userService;

        [BindProperty]
        public TreatmentService NewService { get; set; }

        public CreateModel(TreatmentServiceService treatmentServiceService, UserService userService)
        {
            _treatmentServiceService = treatmentServiceService;
            _userService = userService;
        }

        public void OnGet()
        {
            // You can optionally prepopulate any values if required.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Get current user id from session
                var userIdSession = HttpContext.Session.GetString("UserId");

                if (!string.IsNullOrEmpty(userIdSession))
                {
                    NewService.UserId = Guid.Parse(userIdSession);  // Set the UserId from the session

                    // Create a new treatment service
                    await _treatmentServiceService.CreateTreatmentServiceAsync(NewService);
                    return RedirectToPage("./Index");
                }

                ModelState.AddModelError("", "User is not authenticated.");
                return Page();
            }

            return Page();
        }
    }
}

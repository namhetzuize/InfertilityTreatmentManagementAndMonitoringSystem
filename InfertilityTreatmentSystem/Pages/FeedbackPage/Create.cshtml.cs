using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace InfertilityTreatmentSystem.Pages.FeedbackPage
{
    public class CreateModel : PageModel
    {
        private readonly FeedbackService _feedbackService;
        private readonly UserService _userService;

        [BindProperty]
        public Feedback NewFeedback { get; set; }

        public CreateModel(FeedbackService feedbackService, UserService userService)
        {
            _feedbackService = feedbackService;
            _userService = userService;
        }

        public void OnGet()
        {
            // Optionally pre-populate any values or check session
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Get current user id from session
                var userIdClaim = HttpContext.Session.GetString("UserId");

                if (userIdClaim != null)
                {
                    // Set the CustomerId (UserId) from session and set CreatedDate
                    NewFeedback.CustomerId = Guid.Parse(userIdClaim);
                    NewFeedback.CreatedDate = DateTime.UtcNow;

                    // Create new feedback
                    await _feedbackService.CreateFeedbackAsync(NewFeedback);

                    // Redirect to Index after successful creation
                    return RedirectToPage("./Index");
                }

                // If the user is not authenticated or the UserId is not found in session
                ModelState.AddModelError("", "User is not authenticated.");
                return Page();
            }

            return Page();
        }
    }
}

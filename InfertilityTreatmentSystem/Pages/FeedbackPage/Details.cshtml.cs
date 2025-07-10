using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.FeedbackPage
{
    public class DetailsModel : PageModel
    {
        private readonly FeedbackService _feedbackService;
        private readonly UserService _userService; // Inject UserService to fetch the Customer details

        public DetailsModel(FeedbackService feedbackService, UserService userService)
        {
            _feedbackService = feedbackService;
            _userService = userService;
        }

        public Feedback Feedback { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid feedbackId)
        {
            // Fetch the feedback by ID
            Feedback = await _feedbackService.GetFeedbackByIdAsync(feedbackId);

            if (Feedback == null)
            {
                return NotFound(); // If feedback is not found, return a 404
            }

            // Fetch the associated customer (User) using UserService
            Feedback.Customer = await _userService.GetUserByIdAsync(Feedback.CustomerId);

            return Page(); // Return the page if feedback is found and Customer is loaded
        }
    }
}

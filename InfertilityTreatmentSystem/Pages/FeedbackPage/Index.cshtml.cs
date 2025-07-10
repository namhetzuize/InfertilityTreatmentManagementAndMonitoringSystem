using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.FeedbackPage
{
    public class IndexModel : PageModel
    {
        private readonly FeedbackService _feedbackService;
        private readonly UserService _userService;

        public List<Feedback> Feedbacks { get; set; }

        public IndexModel(FeedbackService feedbackService, UserService userService)
        {
            _feedbackService = feedbackService;
            _userService = userService;
        }

        public async Task OnGetAsync()
        {
            // Fetch all feedback records
            Feedbacks = await _feedbackService.GetAllFeedbacksAsync();

            // For each feedback, populate the associated customer (User) details
            foreach (var feedback in Feedbacks)
            {
                // Get the user associated with the feedback using UserService
                feedback.Customer = await _userService.GetUserByIdAsync(feedback.CustomerId);
            }
        }
    }
}

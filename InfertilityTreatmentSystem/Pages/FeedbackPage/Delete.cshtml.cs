using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.FeedbackPage
{
    public class DeleteModel : PageModel
    {
        private readonly FeedbackService _feedbackService;

        [BindProperty]
        public Feedback Feedback { get; set; }

        public DeleteModel(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<IActionResult> OnGetAsync(Guid feedbackId)
        {
            Feedback = await _feedbackService.GetFeedbackByIdAsync(feedbackId);

            if (Feedback == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Feedback != null)
            {
                // Call DeleteFeedbackByIdAsync for deleting the feedback
                await _feedbackService.DeleteFeedbackByIdAsync(Feedback.FeedbackId);
                return RedirectToPage("./Index");
            }

            return NotFound();
        }
    }
}

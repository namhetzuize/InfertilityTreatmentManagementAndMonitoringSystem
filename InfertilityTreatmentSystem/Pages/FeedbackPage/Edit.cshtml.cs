using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.FeedbackPage
{
    public class EditModel : PageModel
    {
        private readonly FeedbackService _feedbackService;

        [BindProperty]
        public Feedback Feedback { get; set; }

        public EditModel(FeedbackService feedbackService)
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
            if (ModelState.IsValid)
            {
                // Call UpdateFeedbackByIdAsync for updating the feedback
                await _feedbackService.UpdateFeedbackByIdAsync(Feedback.FeedbackId, Feedback);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}

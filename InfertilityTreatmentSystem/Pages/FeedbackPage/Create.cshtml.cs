using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Validate rating range
            if (NewFeedback.Rating < 1 || NewFeedback.Rating > 5)
            {
                ModelState.AddModelError("NewFeedback.Rating", "Rating phải từ 1-5");
                return Page();
            }

            // Validate comment length
            if (string.IsNullOrWhiteSpace(NewFeedback.Comment) || NewFeedback.Comment.Length < 10)
            {
                ModelState.AddModelError("NewFeedback.Comment", "Comment tối thiểu 10 ký tự");
                return Page();
            }

            // Get user ID
            var userId = User.FindFirst("UserId")?.Value ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError("", "Không tìm thấy thông tin user");
                return Page();
            }

            // Set metadata
            NewFeedback.CustomerId = Guid.Parse(userId);
            NewFeedback.CreatedDate = DateTime.Now;

            try
            {
                // Save using service
                var success = await _feedbackService.CreateFeedbackAsync(NewFeedback);

                if (success)
                {
                    TempData["SuccessMessage"] = "Gửi phản hồi thành công!";
                    return Redirect("~/");
                }
                else
                {
                    ModelState.AddModelError("", "Không thể lưu feedback");
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi: {ex.Message}");
                return Page();
            }
        }
    }
}
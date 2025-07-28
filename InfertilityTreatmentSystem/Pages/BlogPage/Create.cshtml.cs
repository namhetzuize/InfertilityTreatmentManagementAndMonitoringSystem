using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;  // To access session
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace InfertilityTreatmentSystem.Pages.BlogPage
{
    [Authorize(Roles = "Admin,Doctor")]
    public class CreateModel : PageModel
    {
        private readonly BlogService _blogService;
        private readonly UserService _userService; // Added user service to set author

        [BindProperty]
        public Blog NewBlog { get; set; }

        public CreateModel(BlogService blogService, UserService userService)
        {
            _blogService = blogService;
            _userService = userService;
        }

        public void OnGet()
        {
            // Additional logic can be added here to set default values or retrieve current user
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // pull the user’s GUID from the auth cookie / claims
            var userIdStr = User.FindFirstValue("UserId")
                           ?? User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr))
            {
                ModelState.AddModelError("", "Unable to identify current user.");
                return Page();
            }

            NewBlog.UserId = Guid.Parse(userIdStr);
            NewBlog.BlogId = Guid.NewGuid();
            NewBlog.CreatedDate = DateTime.Now;

            await _blogService.CreateBlogAsync(NewBlog);
            return RedirectToPage("/BlogPage/Index", new { DoctorId = NewBlog.UserId });
        }
    }
}

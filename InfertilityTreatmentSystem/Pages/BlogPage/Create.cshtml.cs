using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;  // To access session

namespace InfertilityTreatmentSystem.Pages.BlogPage
{
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
            if (ModelState.IsValid)
            {
                // Retrieve UserId from session (set during login)
                var userIdClaim = HttpContext.Session.GetString("UserId");

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    ModelState.AddModelError(string.Empty, "User is not authenticated.");
                    return Page();
                }

                // Set the UserId from session to NewBlog.UserId
                NewBlog.UserId = Guid.Parse(userIdClaim);

                // Set the CreatedDate to the current date and time
                NewBlog.CreatedDate = DateTime.Now;

                // Create the new blog
                await _blogService.CreateBlogAsync(NewBlog);
                return RedirectToPage("/BlogPage/Index");
            }

            return Page();
        }
    }
}

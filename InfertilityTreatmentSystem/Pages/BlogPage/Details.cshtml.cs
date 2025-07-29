using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;

namespace InfertilityTreatmentSystem.Pages.BlogPage
{
    public class DetailsModel : PageModel
    {
        private readonly BlogService _blogService;
        private readonly UserService _userService;

        [BindProperty]
        public Blog Blog { get; set; }
        public User Author { get; set; } // To hold the Author details
        public Guid DoctorId { get; set; }

        public DetailsModel(BlogService blogService, UserService userService)
        {
            _blogService = blogService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(Guid blogId)
        {
            Blog = await _blogService.GetBlogByIdAsync(blogId);
           ;

            if (Blog == null)
            {
                return NotFound();
            }
            DoctorId = Blog.UserId;
            // Fetch the author of the blog
            Author = await _userService.GetUserByIdAsync(Blog.UserId);

            return Page();
        }
    }
}

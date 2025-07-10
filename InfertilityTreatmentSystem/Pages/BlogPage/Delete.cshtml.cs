using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.BlogPage
{
    public class DeleteModel : PageModel
    {
        private readonly BlogService _blogService;

        [BindProperty]
        public Blog Blog { get; set; }

        public DeleteModel(BlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IActionResult> OnGetAsync(Guid blogId)
        {
            // Fetch the blog to be deleted using its BlogId
            Blog = await _blogService.GetBlogByIdAsync(blogId);

            if (Blog == null)
            {
                return NotFound();  // Return NotFound if the blog doesn't exist
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Blog != null)
            {
                // Use the DeleteBlogByIdAsync method to explicitly delete the blog by BlogId
                await _blogService.DeleteBlogByIdAsync(Blog.BlogId);  // Pass BlogId to the service layer
                return RedirectToPage("/BlogPage/Index");  // Redirect to the index page after deletion
            }

            return NotFound();  // Return NotFound if no blog is found
        }
    }
}

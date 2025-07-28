using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace InfertilityTreatmentSystem.Pages.BlogPage
{
    public class EditModel : PageModel
    {
        private readonly BlogService _blogService;
        

        [BindProperty]
        public Blog Blog { get; set; }

        public EditModel(BlogService blogService)
        {
            _blogService = blogService;
        
        }

      
        public async Task<IActionResult> OnGetAsync(Guid blogId)
        {
            Blog = await _blogService.GetBlogByIdAsync(blogId);
            if (Blog == null)
            {
                return NotFound();
            }

          

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Use the UpdateBlogByIdAsync method to explicitly update the blog by its BlogId
                await _blogService.UpdateBlogByIdAsync(Blog.BlogId, Blog);
                return RedirectToPage("/BlogPage/Index");
            }

            return Page();
        }
    }
}

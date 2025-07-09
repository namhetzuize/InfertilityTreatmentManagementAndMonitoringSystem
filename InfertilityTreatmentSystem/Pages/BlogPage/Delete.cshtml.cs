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
            Blog = await _blogService.GetBlogByIdAsync(blogId);

            if (Blog == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Blog != null)
            {
                await _blogService.DeleteBlogAsync(Blog);
                return RedirectToPage("/BlogPage/Index");
            }

            return NotFound();
        }
    }
}

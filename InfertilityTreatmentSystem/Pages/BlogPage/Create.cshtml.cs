using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.BlogPage
{
    public class CreateModel : PageModel
    {
        private readonly BlogService _blogService;

        [BindProperty]
        public Blog NewBlog { get; set; }

        public CreateModel(BlogService blogService)
        {
            _blogService = blogService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                await _blogService.CreateBlogAsync(NewBlog);
                return RedirectToPage("/BlogPage/Index");
            }

            return Page();
        }
    }
}

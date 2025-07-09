using InfertilityTreatmentSystem.BLL.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.Pages.BlogPage
{
    public class IndexModel : PageModel
    {
        private readonly BlogService _blogService;

        public List<Blog> Blogs { get; set; }

        public IndexModel(BlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task OnGetAsync()
        {
            Blogs = await _blogService.GetAllBlogsAsync();
        }
    }
}

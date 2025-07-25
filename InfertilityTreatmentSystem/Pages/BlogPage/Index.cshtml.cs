using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.BlogPage
{
    public class IndexModel : PageModel
    {
        private readonly BlogService _blogService;

        public IndexModel(BlogService blogService)
        {
            _blogService = blogService;
        }

        public List<Blog> Blogs { get; set; } = new();
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;

        // Binds the ?searchTerm= querystring
        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            // Always pull the paged list (no role checks needed)
            var page = await _blogService.GetPagedBlogsAsync(
                searchTerm, pageIndex, PageSize);

            Blogs = page.List;
            PageIndex = page.PageIndex;
            TotalPages = page.TotalPages;
        }
    }
}

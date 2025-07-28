using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.BlogPage
{
    public class IndexModel : PageModel
    {
        private readonly BlogService _blogService;
        public IndexModel(BlogService blogService) => _blogService = blogService;

        public List<Blog> Blogs { get; set; } = new();
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public string searchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? DoctorId { get; set; }

        public async Task OnGetAsync(int pageIndex = 1)
        {
            if (DoctorId.HasValue)
            {
                var list = await _blogService.GetBlogsByUserIdAsync(DoctorId.Value);
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    var st = searchTerm.Trim().ToLower();
                    list = list
                        .Where(b =>
                            (b.Title ?? "").ToLower().Contains(st) ||
                            (b.Content ?? "").ToLower().Contains(st))
                        .ToList();
                }
                Blogs = list;
                PageIndex = 1;
                TotalPages = 1;
            }
            else
            {
                var page = await _blogService.GetPagedBlogsAsync(searchTerm, pageIndex, PageSize);
                Blogs = page.List;
                PageIndex = page.PageIndex;
                TotalPages = page.TotalPages;
            }
        }
    }
}

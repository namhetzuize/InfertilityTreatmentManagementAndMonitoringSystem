using InfertilityTreatmentSystem.BLL.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;

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

       
        public async Task<IActionResult> OnGetAsync()
        {
            var userId = User.FindFirst("UserId")?.Value;
            var role = User.FindFirst("Role")?.Value;

            if (role == "Customer")
            {
                var allBlogs = await _blogService.GetAllBlogsAsync();
                var firstBlog = allBlogs.FirstOrDefault();
                if (firstBlog != null)
                {
                    return RedirectToPage("/BlogPage/Details", new { blogId = firstBlog.BlogId });
                }
                return RedirectToPage("/Home"); // Không có blog
            }

            if (role == "Doctor")
            {
                var allBlogs = await _blogService.GetAllBlogsAsync();
                Blogs = allBlogs.Where(b => b.UserId.ToString() == userId).ToList();
            }
            else if (role == "Admin")
            {
                Blogs = await _blogService.GetAllBlogsAsync();
            }

            return Page();
        }
    }
}

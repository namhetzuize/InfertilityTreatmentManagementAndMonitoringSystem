using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;

namespace InfertilityTreatmentSystem.BLL.Service
{
    public class BlogService
    {
        private readonly UnitOfWork _unitOfWork;

        public BlogService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Blog>> GetBlogsByUserIdAsync(Guid userId)
        {
            var all = await _unitOfWork.BlogRepository.GetAllAsync();
            return all.Where(b => b.UserId == userId).ToList();
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await _unitOfWork.BlogRepository.GetAllAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(Guid id)
        {
            return await _unitOfWork.BlogRepository.GetByIdAsync(id);
        }

        public async Task CreateBlogAsync(Blog blog)
        {
            _unitOfWork.BlogRepository.PrepareCreate(blog);
            await _unitOfWork.BlogRepository.SaveAsync();
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            _unitOfWork.BlogRepository.PrepareUpdate(blog);
            await _unitOfWork.BlogRepository.SaveAsync();
        }

        public async Task DeleteBlogAsync(Blog blog)
        {
            _unitOfWork.BlogRepository.PrepareRemove(blog);
            await _unitOfWork.BlogRepository.SaveAsync();
        }

        // Update Blog by BlogId
        public async Task UpdateBlogByIdAsync(Guid blogId, Blog updatedBlog)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdAsync(blogId);
            if (blog == null)
            {
                throw new Exception("Blog not found.");
            }

            // Update blog properties
            blog.Title = updatedBlog.Title;
            blog.Content = updatedBlog.Content;

            _unitOfWork.BlogRepository.PrepareUpdate(blog);
            await _unitOfWork.BlogRepository.SaveAsync();
        }

        // Delete Blog by BlogId
        public async Task DeleteBlogByIdAsync(Guid blogId)
        {
            var blog = await _unitOfWork.BlogRepository.GetByIdAsync(blogId);
            if (blog == null)
            {
                throw new Exception("Blog not found.");
            }

            _unitOfWork.BlogRepository.PrepareRemove(blog);
            await _unitOfWork.BlogRepository.SaveAsync();
        }

        public async Task<PagingResponse<Blog>> GetPagedBlogsAsync(
           string searchTerm,
           int pageIndex = 1,
           int pageSize = 10)
        {
            return await _unitOfWork.BlogRepository.GetPagedBlogsAsync(searchTerm, pageIndex, pageSize);
        }
    }
}

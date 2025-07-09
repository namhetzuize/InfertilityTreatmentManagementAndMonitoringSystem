using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.BLL.Service
{
    public class BlogService
    {
        private readonly UnitOfWork _unitOfWork;

        public BlogService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await _unitOfWork.BlogRepository.GetAllAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
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
    }
}

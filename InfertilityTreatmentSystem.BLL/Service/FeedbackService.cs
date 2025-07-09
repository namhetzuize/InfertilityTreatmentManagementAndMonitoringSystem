using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.BLL.Service
{
    public class FeedbackService
    {
        private readonly UnitOfWork _unitOfWork;

        public FeedbackService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Feedback>> GetAllFeedbacksAsync()
        {
            return await _unitOfWork.FeedbackRepository.GetAllAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(Guid id)
        {
            return await _unitOfWork.FeedbackRepository.GetByIdAsync(id);
        }

        public async Task CreateFeedbackAsync(Feedback feedback)
        {
            _unitOfWork.FeedbackRepository.PrepareCreate(feedback);
            await _unitOfWork.FeedbackRepository.SaveAsync();
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _unitOfWork.FeedbackRepository.PrepareUpdate(feedback);
            await _unitOfWork.FeedbackRepository.SaveAsync();
        }

        public async Task DeleteFeedbackAsync(Feedback feedback)
        {
            _unitOfWork.FeedbackRepository.PrepareRemove(feedback);
            await _unitOfWork.FeedbackRepository.SaveAsync();
        }
    }
}

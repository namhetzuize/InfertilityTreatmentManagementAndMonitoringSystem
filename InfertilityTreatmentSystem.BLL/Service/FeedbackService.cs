using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;

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

        // Update Feedback by FeedbackId
        public async Task UpdateFeedbackByIdAsync(Guid feedbackId, Feedback updatedFeedback)
        {
            var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(feedbackId);
            if (feedback == null)
            {
                throw new Exception("Feedback not found.");
            }

            // Update feedback properties
            feedback.Rating = updatedFeedback.Rating;
            feedback.Comment = updatedFeedback.Comment;

            _unitOfWork.FeedbackRepository.PrepareUpdate(feedback);
            await _unitOfWork.FeedbackRepository.SaveAsync();
        }

        // Delete Feedback by FeedbackId
        public async Task DeleteFeedbackByIdAsync(Guid feedbackId)
        {
            var feedback = await _unitOfWork.FeedbackRepository.GetByIdAsync(feedbackId);
            if (feedback == null)
            {
                throw new Exception("Feedback not found.");
            }

            _unitOfWork.FeedbackRepository.PrepareRemove(feedback);
            await _unitOfWork.FeedbackRepository.SaveAsync();
        }

        public async Task<PagingResponse<Feedback>> GetPagedFeedbacksAsync(
           string searchTerm,
           int pageIndex = 1,
           int pageSize = 10)
        {
            return await _unitOfWork.FeedbackRepository.GetPagedFeedbacksAsync(searchTerm, pageIndex, pageSize);
        }
    }
}

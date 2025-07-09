using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.BLL.Service
{
    public class TreatmentServiceService
    {
        private readonly UnitOfWork _unitOfWork;

        public TreatmentServiceService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TreatmentService>> GetAllTreatmentServicesAsync()
        {
            return await _unitOfWork.TreatmentServiceRepository.GetAllAsync();
        }

        public async Task<TreatmentService> GetTreatmentServiceByIdAsync(Guid id)
        {
            return await _unitOfWork.TreatmentServiceRepository.GetByIdAsync(id);
        }

        public async Task CreateTreatmentServiceAsync(TreatmentService service)
        {
            _unitOfWork.TreatmentServiceRepository.PrepareCreate(service);
            await _unitOfWork.TreatmentServiceRepository.SaveAsync();
        }

        public async Task UpdateTreatmentServiceAsync(TreatmentService service)
        {
            _unitOfWork.TreatmentServiceRepository.PrepareUpdate(service);
            await _unitOfWork.TreatmentServiceRepository.SaveAsync();
        }

        public async Task DeleteTreatmentServiceAsync(TreatmentService service)
        {
            _unitOfWork.TreatmentServiceRepository.PrepareRemove(service);
            await _unitOfWork.TreatmentServiceRepository.SaveAsync();
        }
    }
}

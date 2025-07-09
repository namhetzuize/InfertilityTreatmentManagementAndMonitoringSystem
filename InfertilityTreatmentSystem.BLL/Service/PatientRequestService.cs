using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.BLL.Service
{
    public class PatientRequestService
    {
        private readonly UnitOfWork _unitOfWork;

        public PatientRequestService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PatientRequest>> GetAllPatientRequestsAsync()
        {
            return await _unitOfWork.PatientRequestRepository.GetAllAsync();
        }

        public async Task<PatientRequest> GetPatientRequestByIdAsync(Guid id)
        {
            return await _unitOfWork.PatientRequestRepository.GetByIdAsync(id);
        }

        public async Task CreatePatientRequestAsync(PatientRequest request)
        {
            _unitOfWork.PatientRequestRepository.PrepareCreate(request);
            await _unitOfWork.PatientRequestRepository.SaveAsync();
        }

        public async Task UpdatePatientRequestAsync(PatientRequest request)
        {
            _unitOfWork.PatientRequestRepository.PrepareUpdate(request);
            await _unitOfWork.PatientRequestRepository.SaveAsync();
        }

        public async Task DeletePatientRequestAsync(PatientRequest request)
        {
            _unitOfWork.PatientRequestRepository.PrepareRemove(request);
            await _unitOfWork.PatientRequestRepository.SaveAsync();
        }
    }
}

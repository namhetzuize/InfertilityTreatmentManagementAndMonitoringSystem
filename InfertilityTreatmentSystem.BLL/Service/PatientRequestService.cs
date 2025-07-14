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

        // Update PatientRequest by RequestId
        public async Task UpdatePatientRequestByIdAsync(Guid requestId, PatientRequest updatedRequest)
        {
            var request = await _unitOfWork.PatientRequestRepository.GetByIdAsync(requestId);
            if (request == null)
            {
                throw new Exception("Request not found.");
            }

            // Update the request properties
            request.CustomerId = updatedRequest.CustomerId;
            request.DoctorId = updatedRequest.DoctorId;
            request.ServiceId = updatedRequest.ServiceId;
            request.Note = updatedRequest.Note;
            request.RequestedDate = updatedRequest.RequestedDate;
            request.CreatedDate = updatedRequest.CreatedDate;

            _unitOfWork.PatientRequestRepository.PrepareUpdate(request);
            await _unitOfWork.PatientRequestRepository.SaveAsync();
        }

        // Delete PatientRequest by RequestId
        public async Task DeletePatientRequestByIdAsync(Guid requestId)
        {
            var request = await _unitOfWork.PatientRequestRepository.GetByIdAsync(requestId);
            if (request == null)
            {
                throw new Exception("Request not found.");
            }

            _unitOfWork.PatientRequestRepository.PrepareRemove(request);
            await _unitOfWork.PatientRequestRepository.SaveAsync();
        }
        public async Task<List<PatientRequest>> GetPatientRequestsByCustomerAndDoctorAsync(Guid customerId, Guid doctorId)
        {
            var allPatientRequests = await _unitOfWork.PatientRequestRepository.GetAllAsync();

            return allPatientRequests
                .Where(r => r.CustomerId == customerId && r.DoctorId == doctorId)
                .ToList();
        }
    }
}

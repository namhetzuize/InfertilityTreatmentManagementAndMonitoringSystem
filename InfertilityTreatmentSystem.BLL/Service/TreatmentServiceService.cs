using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;

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
        public async Task<TreatmentService> GetServiceByServiceNameAsync(string serviceName)
        {
            return await _unitOfWork.TreatmentServiceRepository.GetServiceByServiceNameAsync(serviceName);
        }

        // Update TreatmentService by ServiceId
        public async Task UpdateTreatmentServiceByIdAsync(Guid serviceId, TreatmentService updatedService)
        {
            var service = await _unitOfWork.TreatmentServiceRepository.GetByIdAsync(serviceId);
            if (service == null)
            {
                throw new Exception("Service not found.");
            }

            // Update the service properties
            service.ServiceName = updatedService.ServiceName;
            service.Description = updatedService.Description;
            service.Price = updatedService.Price;

            _unitOfWork.TreatmentServiceRepository.PrepareUpdate(service);
            await _unitOfWork.TreatmentServiceRepository.SaveAsync();
        }

        // Delete TreatmentService by ServiceId
        public async Task DeleteTreatmentServiceByIdAsync(Guid serviceId)
        {
            var service = await _unitOfWork.TreatmentServiceRepository.GetByIdAsync(serviceId);
            if (service == null)
            {
                throw new Exception("Service not found.");
            }

            _unitOfWork.TreatmentServiceRepository.PrepareRemove(service);
            await _unitOfWork.TreatmentServiceRepository.SaveAsync();
        }

        public async Task<TreatmentService?> GetServiceByServiceIdAsync(Guid serviceId)
        {
            return await _unitOfWork.TreatmentServiceRepository.GetByIdAsync(serviceId);
        }

        public async Task<PagingResponse<TreatmentService>> GetPagedTreatmentServicesAsync(
            string searchTerm,
            int pageIndex = 1,
            int pageSize = 10)
        {
            return await _unitOfWork.TreatmentServiceRepository.GetPagedTreatmentServicesAsync(searchTerm, pageIndex, pageSize);
        }
        public async Task<TreatmentService?> GetByIdWithRelationsAsync(Guid serviceId)
        {
            return await _unitOfWork.TreatmentServiceRepository.GetByIdWithRelationsAsync(serviceId);
        }

    }
}

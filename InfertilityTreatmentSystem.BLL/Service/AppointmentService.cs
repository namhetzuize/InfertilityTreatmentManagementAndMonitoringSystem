using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.BLL.Service
{
    public class AppointmentService
    {
        private readonly UnitOfWork _unitOfWork;

        public AppointmentService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _unitOfWork.AppointmentRepository.GetAllAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(Guid id)
        {
            return await _unitOfWork.AppointmentRepository.GetByIdAsync(id);
        }

        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            _unitOfWork.AppointmentRepository.PrepareCreate(appointment);
            await _unitOfWork.AppointmentRepository.SaveAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _unitOfWork.AppointmentRepository.PrepareUpdate(appointment);
            await _unitOfWork.AppointmentRepository.SaveAsync();
        }

        public async Task DeleteAppointmentAsync(Appointment appointment)
        {
            _unitOfWork.AppointmentRepository.PrepareRemove(appointment);
            await _unitOfWork.AppointmentRepository.SaveAsync();
        }
    }
}

using InfertilityTreatmentSystem.DAL;
using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.DAL.Paging;
using Microsoft.EntityFrameworkCore;

namespace InfertilityTreatmentSystem.BLL.Service
{
    public class AppointmentService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly UserService _userService;
        private readonly TreatmentServiceService _treatmentServiceService;

        public AppointmentService(UnitOfWork unitOfWork, UserService userService, TreatmentServiceService treatmentServiceService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAsync();

            // Loop through each appointment and fetch the related Customer and Doctor
            foreach (var appointment in appointments)
            {
                // Fetch Customer by CustomerId
                appointment.Customer = await _userService.GetUserByIdAsync(appointment.CustomerId);

                // Fetch Doctor by DoctorId
                appointment.Doctor = await _userService.GetUserByIdAsync(appointment.DoctorId);
            }

            return appointments;
        }

        public async Task<(List<User> Customers, List<User> Doctors)> GetCustomersAndDoctorsAsync()
        {
            var customers = await _unitOfWork.UserRepository.GetAllAsync();
            var doctors = customers.Where(u => u.Role == "Doctor").ToList();
            customers = customers.Where(u => u.Role == "Customer").ToList();

            return (customers, doctors);
        }

        // in InfertilityTreatmentSystem.BLL.Service/AppointmentService.cs

        public async Task<Appointment> GetAppointmentByIdAsync(Guid id)
        {
            // 1) load the bare appointment
            var appt = await _unitOfWork.AppointmentRepository.GetByIdAsync(id);
            if (appt == null) return null;

            // 2) populate Customer & Doctor via UserService
            appt.Customer = await _userService.GetUserByIdAsync(appt.CustomerId);
            appt.Doctor = await _userService.GetUserByIdAsync(appt.DoctorId);
            appt.Service = await _treatmentServiceService.GetTreatmentServiceByIdAsync(appt.ServiceId);

            // 3) populate the TreatmentService
            appt.Service = await _unitOfWork.TreatmentServiceRepository.GetByIdAsync(appt.ServiceId);

            return appt;
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

        // Update Appointment by AppointmentId
        public async Task UpdateAppointmentByIdAsync(Guid appointmentId, Appointment updatedAppointment)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found.");
            }

            // Update appointment properties
            appointment.CustomerId = updatedAppointment.CustomerId;
            appointment.DoctorId = updatedAppointment.DoctorId;
            appointment.ServiceId = updatedAppointment.ServiceId;
            appointment.AppointmentDate = updatedAppointment.AppointmentDate;
            appointment.Status = updatedAppointment.Status;

            _unitOfWork.AppointmentRepository.PrepareUpdate(appointment);
            await _unitOfWork.AppointmentRepository.SaveAsync();
        }

        // Delete Appointment by AppointmentId
        public async Task DeleteAppointmentByIdAsync(Guid appointmentId)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(appointmentId);
            if (appointment == null)
            {
                throw new Exception("Appointment not found.");
            }

            _unitOfWork.AppointmentRepository.PrepareRemove(appointment);
            await _unitOfWork.AppointmentRepository.SaveAsync();
        }
        public async Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(Guid doctorId)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAsync();

            var doctorAppointments = appointments
                .Where(a => a.DoctorId == doctorId)
                .ToList();

            foreach (var appointment in doctorAppointments)
            {
                appointment.Customer = await _userService.GetUserByIdAsync(appointment.CustomerId);
                appointment.Doctor = await _userService.GetUserByIdAsync(appointment.DoctorId);
                appointment.Service = await _treatmentServiceService.GetTreatmentServiceByIdAsync(appointment.ServiceId);
            }

            return doctorAppointments;
        }
        public async Task<List<Appointment>> GetAppointmentsByCustomerIdAsync(Guid customerId)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAsync();
            var customerAppointments = appointments
                .Where(a => a.CustomerId == customerId)
                .ToList();
            foreach (var appointment in customerAppointments)
            {
                appointment.Customer = await _userService.GetUserByIdAsync(appointment.CustomerId);
                appointment.Doctor = await _userService.GetUserByIdAsync(appointment.DoctorId);
                appointment.Service = await _treatmentServiceService.GetTreatmentServiceByIdAsync(appointment.ServiceId);
            }
            return customerAppointments;
        }
        public async Task UpdateStatusAppointmentAsync(Guid appointmentId, Appointment updatedAppointment)
        {
            var existingAppointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(updatedAppointment.AppointmentId);

            if (existingAppointment != null)
            {
                existingAppointment.Status = updatedAppointment.Status;
                existingAppointment.RejectReason = updatedAppointment.RejectReason;
                existingAppointment.AppointmentDate = updatedAppointment.AppointmentDate;

                await _unitOfWork.AppointmentRepository.SaveAsync();  // hoặc SaveChangesAsync nếu đó là phương thức bạn dùng
            }
        }
        public async Task<Appointment> GetAppointmentByCustomerAndDoctorAsync(Guid customerId, Guid doctorId)
        {
            var appointments = await _unitOfWork.AppointmentRepository.GetAllAsync();
            return appointments.FirstOrDefault(a => a.CustomerId == customerId && a.DoctorId == doctorId);
        }

        public async Task<PagingResponse<Appointment>> GetPagedAppointmentsAsync(
            string searchTerm,
            int pageIndex = 1,
            int pageSize = 10)
        {
            return await _unitOfWork.AppointmentRepository.GetPagedAppointmentsAsync(searchTerm, pageIndex, pageSize);
        }
    }
}

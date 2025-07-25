using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.MedicalRecordPage
{
    public class DetailsModel : PageModel
    {
        private readonly MedicalRecordService _medicalRecordService;
        private readonly UserService _userService;
        private readonly AppointmentService _appointmentService;
        public DetailsModel(
            MedicalRecordService medicalRecordService,
            UserService userService,
            AppointmentService appointmentService)
        {
            _medicalRecordService = medicalRecordService;
            _userService = userService;
            _appointmentService = appointmentService;
        }

        public MedicalRecord MedicalRecord { get; set; }
        public User Customer { get; set; }
        public User Doctor { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid RecordId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid AppointmentId { get; set; }

        public Guid CustomerId { get; set; }
        public Guid DoctorId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Lấy lịch khám
            MedicalRecord = await _medicalRecordService.GetMedicalRecordByIdAsync(RecordId);
            if (MedicalRecord == null)
            {
                return NotFound();
            }

            // Lấy appointment → để truy customerId và doctorId
            var appointment = await _appointmentService.GetAppointmentByIdAsync(AppointmentId);
            if (appointment == null)
            {
                return NotFound();
            }

            CustomerId = appointment.CustomerId;
            DoctorId = appointment.DoctorId;

            // Lấy thông tin người dùng
            Customer = await _userService.GetUserByIdAsync(CustomerId);
            Doctor = await _userService.GetUserByIdAsync(DoctorId);

            return Page();
        }
    }
}

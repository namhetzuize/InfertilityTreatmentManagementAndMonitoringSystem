using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages
{
    public class MedicalProfileDetailsModel : PageModel
    {
        private readonly ScheduleService _scheduleService;
        private readonly PatientRequestService _patientRequestService;
        private readonly MedicalRecordService _medicalRecordService;
        private readonly UserService _userService;
        private readonly AppointmentService _appointmentService;

        public MedicalProfileDetailsModel(ScheduleService scheduleService,
                            PatientRequestService patientRequestService,
                            MedicalRecordService medicalRecordService,
                            UserService userService, AppointmentService appointmentService
            )
        {
            _scheduleService = scheduleService;
            _patientRequestService = patientRequestService;
            _medicalRecordService = medicalRecordService;
            _userService = userService;
            _appointmentService = appointmentService;
        }
        [BindProperty]
        public MedicalRecord NewMedicalRecord { get; set; } = new();
        [BindProperty]
        public Schedule NewSchedule { get; set; } = new();
        public Guid DoctorId { get; set; }
        public User Customer { get; set; }
        public List<Schedule> Schedules { get; set; } = new();
        public List<PatientRequest> PatientRequests { get; set; } = new();
        public List<MedicalRecord> MedicalRecords { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid AppointmentId { get; set; }

        public Appointment Appointment { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid customerId, Guid doctorId)
        {
            DoctorId = doctorId;
            Customer = await _userService.GetUserByIdAsync(customerId);

            Appointment = await _appointmentService.GetAppointmentByCustomerAndDoctorAsync(customerId, doctorId);
            AppointmentId = Appointment.AppointmentId;
            Schedules = await _scheduleService.GetSchedulesByCustomerAndDoctorAsync(customerId, doctorId);
            PatientRequests = await _patientRequestService.GetPatientRequestsByCustomerAndDoctorAsync(customerId, doctorId);
            MedicalRecords = await _medicalRecordService.GetMedicalRecordsByCustomerAndDoctorAsync(customerId, doctorId);

            return Page();
        }
        public async Task<IActionResult> OnPostCreateScheduleAsync(Guid customerId, Guid doctorId)
        {
            var appointment = await _appointmentService
                
        .GetAppointmentByCustomerAndDoctorAsync(customerId, doctorId);
            var now = DateTime.Now;

            if (!NewSchedule.ScheduleDate.HasValue)
            {
                ModelState.AddModelError("NewSchedule.ScheduleDate", "Vui lòng chọn ngày khám.");
            }
            else
            {
                var scheduleDate = NewSchedule.ScheduleDate.Value;

                if (scheduleDate <= now)
                {
                    ModelState.AddModelError("NewSchedule.ScheduleDate", "Ngày khám phải lớn hơn thời gian hiện tại.");
                }

                if (scheduleDate.Hour < 8 || scheduleDate.Hour >= 17)
                {
                    ModelState.AddModelError("NewSchedule.ScheduleDate", "Giờ khám phải trong khoảng từ 08:00 đến 17:00.");
                }
            }

            if (string.IsNullOrWhiteSpace(NewSchedule.SerivceName))
            {
                ModelState.AddModelError("NewSchedule.SerivceName", "Dịch vụ không được để trống.");
            }

            NewSchedule.CustomerId = customerId;
            NewSchedule.DoctorId = doctorId;
            NewSchedule.AppointmentId = appointment.AppointmentId;

            await _scheduleService.CreateScheduleAsync(NewSchedule);

            return RedirectToPage(new { customerId, doctorId });
        }
        public async Task<IActionResult> OnPostCreateMedicalRecordAsync(Guid customerId, Guid doctorId)
        {
            var appointment = await _appointmentService.GetAppointmentByCustomerAndDoctorAsync(customerId, doctorId);
            if (!ModelState.IsValid)
            {
                Customer = await _userService.GetUserByIdAsync(customerId);
                DoctorId = doctorId;

                Schedules = await _scheduleService.GetSchedulesByCustomerAndDoctorAsync(customerId, doctorId);
                PatientRequests = await _patientRequestService.GetPatientRequestsByCustomerAndDoctorAsync(customerId, doctorId);
                MedicalRecords = await _medicalRecordService.GetMedicalRecordsByCustomerAndDoctorAsync(customerId, doctorId);

                return Page();
            }

            NewMedicalRecord.CustomerId = customerId;
            NewMedicalRecord.DoctorId = doctorId;
            NewMedicalRecord.CreatedDate = DateTime.Now;
            NewMedicalRecord.AppointmentId = appointment.AppointmentId;

            await _medicalRecordService.CreateMedicalRecordAsync(NewMedicalRecord);

            return RedirectToPage(new { customerId, doctorId });
        }
    }
}

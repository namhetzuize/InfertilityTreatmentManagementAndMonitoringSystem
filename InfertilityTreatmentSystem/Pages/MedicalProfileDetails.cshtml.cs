using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InfertilityTreatmentSystem.Pages
{
    public class MedicalProfileDetailsModel : PageModel
    {
        private readonly ScheduleService _scheduleService;
        private readonly PatientRequestService _patientRequestService;
        private readonly MedicalRecordService _medicalRecordService;
        private readonly UserService _userService;
        private readonly AppointmentService _appointmentService;
        private readonly TreatmentServiceService _treatmentServiceService;

        public MedicalProfileDetailsModel(ScheduleService scheduleService,
                            PatientRequestService patientRequestService,
                            MedicalRecordService medicalRecordService,
                            UserService userService, AppointmentService appointmentService, TreatmentServiceService treatmentServiceService
            )
        {
            _scheduleService = scheduleService;
            _patientRequestService = patientRequestService;
            _medicalRecordService = medicalRecordService;
            _userService = userService;
            _appointmentService = appointmentService;
            _treatmentServiceService = treatmentServiceService;
        }
        [BindProperty]
        public MedicalRecord NewMedicalRecord { get; set; } = new();
        [BindProperty]
        public Schedule NewSchedule { get; set; } = new();
        public User Customer { get; set; }
        public List<Schedule> Schedules { get; set; } = new();
        public List<PatientRequest> PatientRequests { get; set; } = new();
        public List<MedicalRecord> MedicalRecords { get; set; } = new();
        public List<TreatmentService> TreatmentServices { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public Guid CustomerId { get; set; }
        [BindProperty(SupportsGet = true)]
        public Guid DoctorId { get; set; }

        [BindProperty]
        public PatientRequest NewRequest { get; set; }
        public List<User> Customers { get; set; }
        public List<User> Doctors { get; set; }
        
        public List<SelectListItem> ServiceItems { get; set; }
        [BindProperty]
        public PatientRequest NewPatientRequest { get; set; }


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
            Customers = await _userService.GetAllUsersAsync();
            Doctors = Customers.Where(u => u.Role == "Doctor").ToList();
            TreatmentServices = await _treatmentServiceService.GetAllTreatmentServicesAsync();
            PatientRequests = await _patientRequestService
               .GetPatientRequestsByCustomerAndDoctorAsync(CustomerId, DoctorId);

            // populate dropdown
            var allSvc = await _treatmentServiceService.GetAllTreatmentServicesAsync();
            ServiceItems = allSvc
                .Select(s => new SelectListItem(s.ServiceName, s.ServiceId.ToString()))
                .ToList();


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

        public async Task<IActionResult> OnPostCreateRequestAsync(Guid customerId, Guid doctorId)
        {
            var allSvc = await _treatmentServiceService.GetAllTreatmentServicesAsync();
            ServiceItems = allSvc
                .Select(s => new SelectListItem
                {
                    Text = s.ServiceName,
                    Value = s.ServiceId.ToString(),
                    Selected = (s.ServiceId == NewPatientRequest.ServiceId)
                })
                .ToList();
            if (NewPatientRequest.ServiceId == Guid.Empty)
                ModelState.AddModelError(nameof(NewPatientRequest.ServiceId), "Vui lòng chọn dịch vụ.");

            if (string.IsNullOrWhiteSpace(NewPatientRequest.Note))
                ModelState.AddModelError(nameof(NewPatientRequest.Note), "Ghi chú không được để trống.");

            if (!ModelState.IsValid)
            {
                // ✅ Load lại các dữ liệu cần thiết để tránh lỗi null khi render View
                Customer = await _userService.GetUserByIdAsync(customerId);
                DoctorId = doctorId;

                Schedules = await _scheduleService.GetSchedulesByCustomerAndDoctorAsync(customerId, doctorId);
                PatientRequests = await _patientRequestService.GetPatientRequestsByCustomerAndDoctorAsync(customerId, doctorId);
                MedicalRecords = await _medicalRecordService.GetMedicalRecordsByCustomerAndDoctorAsync(customerId, doctorId);
                Customers = await _userService.GetAllUsersAsync();
                Doctors = Customers.Where(u => u.Role == "Doctor").ToList();

                return Page();
            }

            NewPatientRequest.RequestId = Guid.NewGuid();
            NewPatientRequest.CustomerId = customerId;
            NewPatientRequest.DoctorId = doctorId;
            NewPatientRequest.RequestedDate = DateTime.Now;
            NewPatientRequest.CreatedDate = DateTime.Now;

            await _patientRequestService.CreatePatientRequestAsync(NewPatientRequest);

            return RedirectToPage(new { customerId, doctorId });
        }

    }
}

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

        public MedicalProfileDetailsModel(ScheduleService scheduleService,
                            PatientRequestService patientRequestService,
                            MedicalRecordService medicalRecordService,
                            UserService userService)
        {
            _scheduleService = scheduleService;
            _patientRequestService = patientRequestService;
            _medicalRecordService = medicalRecordService;
            _userService = userService;
        }

        public User Customer { get; set; }
        public List<Schedule> Schedules { get; set; } = new();
        public List<PatientRequest> PatientRequests { get; set; } = new();
        public List<MedicalRecord> MedicalRecords { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(Guid customerId, Guid doctorId)
        {
            Customer = await _userService.GetUserByIdAsync(customerId);
            Schedules = await _scheduleService.GetSchedulesByCustomerAndDoctorAsync(customerId, doctorId);
            PatientRequests = await _patientRequestService.GetPatientRequestsByCustomerAndDoctorAsync(customerId, doctorId);
            MedicalRecords = await _medicalRecordService.GetMedicalRecordsByCustomerAndDoctorAsync(customerId, doctorId);

            return Page();
        }
    }
}

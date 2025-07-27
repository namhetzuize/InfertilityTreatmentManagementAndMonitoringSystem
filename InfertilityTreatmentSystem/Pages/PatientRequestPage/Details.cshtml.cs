using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.PatientRequestPage
{
    public class DetailsModel : PageModel
    {
        private readonly PatientRequestService _patientRequestService;
        private readonly UserService _userService;
        private readonly TreatmentServiceService _treatmentServiceService;

        public DetailsModel(
            PatientRequestService patientRequestService,
            UserService userService,
            TreatmentServiceService treatmentServiceService)
        {
            _patientRequestService = patientRequestService;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
        }

        public PatientRequest PatientRequest { get; set; }

        public User Customer { get; set; }
        public User Doctor { get; set; }
        public TreatmentService Service { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid RequestId { get; set; }

        public Guid DoctorId { get; set; }
        public Guid CustomerId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            PatientRequest = await _patientRequestService.GetPatientRequestByIdAsync(RequestId);
            if (PatientRequest == null)
            {
                return NotFound();
            }

            // Lấy thông tin liên quan
            Customer = await _userService.GetUserByIdAsync(PatientRequest.CustomerId);
            Doctor = await _userService.GetUserByIdAsync(PatientRequest.DoctorId);
            PatientRequest.Service = await _treatmentServiceService.GetTreatmentServiceByIdAsync(PatientRequest.ServiceId);

            CustomerId = PatientRequest.CustomerId;
            DoctorId = PatientRequest.DoctorId;
            return Page();
        }
    }
}

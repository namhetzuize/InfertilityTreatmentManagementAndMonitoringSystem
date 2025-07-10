using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.PatientRequestPage
{
    public class IndexModel : PageModel
    {
        private readonly PatientRequestService _patientRequestService;
        private readonly UserService _userService;
        private readonly TreatmentServiceService _treatmentServiceService;

        public List<PatientRequest> PatientRequests { get; set; }

        public IndexModel(PatientRequestService patientRequestService, UserService userService, TreatmentServiceService treatmentServiceService)
        {
            _patientRequestService = patientRequestService;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
        }

        public async Task OnGetAsync()
        {
            PatientRequests = await _patientRequestService.GetAllPatientRequestsAsync();

            // Fetch related customer, doctor, and service details
            foreach (var request in PatientRequests)
            {
                request.Customer = await _userService.GetUserByIdAsync(request.CustomerId);
                request.Doctor = await _userService.GetUserByIdAsync(request.DoctorId);
                request.Service = await _treatmentServiceService.GetTreatmentServiceByIdAsync(request.ServiceId);
            }
        }
    }
}

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

        public PatientRequest Request { get; set; }

        public DetailsModel(PatientRequestService patientRequestService, UserService userService, TreatmentServiceService treatmentServiceService)
        {
            _patientRequestService = patientRequestService;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
        }

        public async Task<IActionResult> OnGetAsync(Guid requestId)
        {
            // Get the patient request by its ID
            Request = await _patientRequestService.GetPatientRequestByIdAsync(requestId);

            if (Request == null)
            {
                return NotFound(); // Return a 404 page if the request is not found
            }

            // Fetch related customer, doctor, and service details
            Request.Customer = await _userService.GetUserByIdAsync(Request.CustomerId);
            Request.Doctor = await _userService.GetUserByIdAsync(Request.DoctorId);
            Request.Service = await _treatmentServiceService.GetTreatmentServiceByIdAsync(Request.ServiceId);

            return Page(); // Return the page with all the details
        }
    }
}

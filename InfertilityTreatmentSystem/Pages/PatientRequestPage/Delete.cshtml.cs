using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.PatientRequestPage
{
    public class DeleteModel : PageModel
    {
        private readonly PatientRequestService _patientRequestService;

        [BindProperty]
        public PatientRequest Request { get; set; }

        public DeleteModel(PatientRequestService patientRequestService)
        {
            _patientRequestService = patientRequestService;
        }

        public async Task<IActionResult> OnGetAsync(Guid requestId)
        {
            // Retrieve the medical record by its RecordId
            Request = await _patientRequestService.GetPatientRequestByIdAsync(requestId);

            if (Request == null)
            {
                return NotFound(); // If no record is found, return NotFound
            }

            return Page(); // Return the page if the record is found
        }

        public async Task<IActionResult> OnPostAsync(Guid requestId)
        {
            var request = await _patientRequestService.GetPatientRequestByIdAsync(requestId);
            if (Request != null)
            {
                Guid customerId = request.CustomerId;
                Guid doctorId = request.DoctorId;
                await _patientRequestService.DeletePatientRequestByIdAsync(Request.RequestId);
                return RedirectToPage("/MedicalProfileDetails", new { customerId = customerId, doctorId = doctorId });
            }

            return NotFound();
        }
    }
}

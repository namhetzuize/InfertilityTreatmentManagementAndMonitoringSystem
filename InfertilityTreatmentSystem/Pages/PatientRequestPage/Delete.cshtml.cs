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
            Request = await _patientRequestService.GetPatientRequestByIdAsync(requestId);

            if (Request == null)
            {
                return NotFound(); // Return 404 if the record is not found
            }

            return Page(); // Return the page with details
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Request != null)
            {
                // Delete the request using DeletePatientRequestByIdAsync
                await _patientRequestService.DeletePatientRequestByIdAsync(Request.RequestId);
                return RedirectToPage("./Index");
            }

            return NotFound(); // Return 404 if the record is not found
        }
    }
}

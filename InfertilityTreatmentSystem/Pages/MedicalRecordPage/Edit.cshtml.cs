using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.MedicalRecordPage
{
    public class EditModel : PageModel
    {
        private readonly MedicalRecordService _medicalRecordService;

        [BindProperty]
        public MedicalRecord Record { get; set; }

        public EditModel(MedicalRecordService medicalRecordService)
        {
            _medicalRecordService = medicalRecordService;
        }

        public async Task<IActionResult> OnGetAsync(Guid recordId)
        {
            // Retrieve the medical record by its RecordId
            Record = await _medicalRecordService.GetMedicalRecordByIdAsync(recordId);

            if (Record == null)
            {
                return NotFound(); // If no record is found, return NotFound
            }

            return Page(); // Return the page if the record is found
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Update the medical record using UpdateMedicalRecordByIdAsync
                await _medicalRecordService.UpdateMedicalRecordByIdAsync(Record.RecordId, Record);
                return RedirectToPage("./Index"); // Redirect to the index page after update
            }

            return Page(); // Return the page if the model is invalid
        }
    }
}

using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.MedicalRecordPage
{
    public class DeleteModel : PageModel
    {
        private readonly MedicalRecordService _medicalRecordService;

        [BindProperty]
        public MedicalRecord Record { get; set; }

        public DeleteModel(MedicalRecordService medicalRecordService)
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
            if (Record != null)
            {
                // Delete the medical record using DeleteMedicalRecordByIdAsync
                await _medicalRecordService.DeleteMedicalRecordByIdAsync(Record.RecordId);
                return RedirectToPage("./Index"); // Redirect to the index page after deletion
            }

            return NotFound(); // If the record is not found, return NotFound
        }
    }
}

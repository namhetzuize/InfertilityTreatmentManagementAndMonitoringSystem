using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.MedicalRecordPage
{
    public class IndexModel : PageModel
    {
        private readonly MedicalRecordService _medicalRecordService;
        private readonly UserService _userService;

        public List<MedicalRecord> MedicalRecords { get; set; }

        public IndexModel(MedicalRecordService medicalRecordService, UserService userService)
        {
            _medicalRecordService = medicalRecordService;
            _userService = userService;
        }

        public async Task OnGetAsync()
        {
            // Get all medical records
            MedicalRecords = await _medicalRecordService.GetAllMedicalRecordsAsync();

            // Loop through the records and fetch Customer and Doctor details
            foreach (var record in MedicalRecords)
            {
                // Get Customer and Doctor names
                record.Customer = await _userService.GetUserByIdAsync(record.CustomerId);
                record.Doctor = await _userService.GetUserByIdAsync(record.DoctorId);
            }
        }
    }
}

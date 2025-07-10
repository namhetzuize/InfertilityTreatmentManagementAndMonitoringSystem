using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.MedicalRecordPage
{
    public class DetailsModel : PageModel
    {
        private readonly MedicalRecordService _medicalRecordService;
        private readonly UserService _userService;

        public MedicalRecord MedicalRecord { get; set; }

        public DetailsModel(MedicalRecordService medicalRecordService, UserService userService)
        {
            _medicalRecordService = medicalRecordService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(Guid recordId)
        {
            // Fetch the medical record by ID
            MedicalRecord = await _medicalRecordService.GetMedicalRecordByIdAsync(recordId);

            if (MedicalRecord == null)
            {
                return NotFound(); // If the record is not found, return NotFound()
            }

            // Fetch the related Customer and Doctor information
            MedicalRecord.Customer = await _userService.GetUserByIdAsync(MedicalRecord.CustomerId);
            MedicalRecord.Doctor = await _userService.GetUserByIdAsync(MedicalRecord.DoctorId);

            return Page(); // Return the page if all data is fetched successfully
        }
    }
}

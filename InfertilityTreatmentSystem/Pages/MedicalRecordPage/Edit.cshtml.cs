using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace InfertilityTreatmentSystem.Pages.MedicalRecordPage
{
    public class EditModel : PageModel
    {
        private readonly MedicalRecordService _medicalRecordService;
        private readonly UserService _userService;

        [BindProperty]
        public MedicalRecord Record { get; set; }

        public List<User> Customers { get; set; }
        public List<User> Doctors { get; set; }

        public EditModel(MedicalRecordService medicalRecordService, UserService userService)
        {
            _medicalRecordService = medicalRecordService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(Guid recordId, Guid appointmentId)
        {
            Record = await _medicalRecordService.GetMedicalRecordByIdAsync(recordId);
            if (Record == null)
            {
                return NotFound();
            }
            Record.AppointmentId = appointmentId;

            Customers = await _userService.GetAllUsersAsync();
            Doctors = Customers.Where(u => u.Role == "Doctor").ToList();
            Customers = Customers.Where(u => u.Role == "Customer").ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Customers = await _userService.GetAllUsersAsync();
                Doctors = Customers.Where(u => u.Role == "Doctor").ToList();
                Customers = Customers.Where(u => u.Role == "Customer").ToList();

                return Page();
            }
            var existingRecord = await _medicalRecordService.GetMedicalRecordByIdAsync(Record.RecordId);
            if (existingRecord == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy hồ sơ để cập nhật.");
                return Page();
            }
            existingRecord.Prescription = Record.Prescription;
            existingRecord.TestResults = Record.TestResults;
            existingRecord.Note = Record.Note;
           
            await _medicalRecordService.UpdateMedicalRecordAsync(existingRecord);
            return RedirectToPage("/MedicalRecordPage/Details", new { recordId = Record.RecordId, appointmentId = Record.AppointmentId });
        }
    }
}

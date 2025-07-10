using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.MedicalRecordPage
{
    public class CreateModel : PageModel
    {
        private readonly MedicalRecordService _medicalRecordService;
        private readonly AppointmentService _appointmentService;
        private readonly UserService _userService;

        [BindProperty]
        public MedicalRecord NewMedicalRecord { get; set; }

        public List<User> Doctors { get; set; }
        public List<User> Customers { get; set; }

        public CreateModel(MedicalRecordService medicalRecordService, AppointmentService appointmentService, UserService userService)
        {
            _medicalRecordService = medicalRecordService;
            _appointmentService = appointmentService;
            _userService = userService;
        }

        public async Task OnGetAsync()
        {
            // Load customers and doctors
            var allUsers = await _userService.GetAllUsersAsync();
            Doctors = allUsers.FindAll(u => u.Role == "Doctor");
            Customers = allUsers.FindAll(u => u.Role == "Customer");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Set the creation date
                NewMedicalRecord.CreatedDate = DateTime.UtcNow;

                // Create the new medical record
                await _medicalRecordService.CreateMedicalRecordAsync(NewMedicalRecord);

                return RedirectToPage("./Index"); // Redirect to the list page after creation
            }

            return Page();
        }
    }
}

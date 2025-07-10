using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace InfertilityTreatmentSystem.Pages.PatientRequestPage
{
    public class CreateModel : PageModel
    {
        private readonly PatientRequestService _patientRequestService;
        private readonly UserService _userService;
        private readonly TreatmentServiceService _treatmentServiceService;

        [BindProperty]
        public PatientRequest NewRequest { get; set; }

        public List<User> Customers { get; set; }
        public List<User> Doctors { get; set; }
        public List<TreatmentService> Services { get; set; }

        public CreateModel(PatientRequestService patientRequestService, UserService userService, TreatmentServiceService treatmentServiceService)
        {
            _patientRequestService = patientRequestService;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
        }

        public async Task OnGetAsync()
        {
            // Populate drop-down lists for Customer, Doctor, and Service
            Customers = await _userService.GetAllUsersAsync();
            Doctors = Customers.Where(u => u.Role == "Doctor").ToList();
            Services = await _treatmentServiceService.GetAllTreatmentServicesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Set the requested date and created date
                NewRequest.RequestedDate = DateTime.Now;
                NewRequest.CreatedDate = DateTime.Now;

                // Create the new request
                await _patientRequestService.CreatePatientRequestAsync(NewRequest);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}

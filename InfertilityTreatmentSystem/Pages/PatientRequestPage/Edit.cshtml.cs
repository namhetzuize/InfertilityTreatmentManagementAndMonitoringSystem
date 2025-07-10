using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.PatientRequestPage
{
    public class EditModel : PageModel
    {
        private readonly PatientRequestService _patientRequestService;
        private readonly UserService _userService;
        private readonly TreatmentServiceService _treatmentServiceService;

        [BindProperty]
        public PatientRequest Request { get; set; }

        public List<User> Customers { get; set; }
        public List<User> Doctors { get; set; }
        public List<TreatmentService> Services { get; set; }

        public EditModel(PatientRequestService patientRequestService, UserService userService, TreatmentServiceService treatmentServiceService)
        {
            _patientRequestService = patientRequestService;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
        }

        public async Task<IActionResult> OnGetAsync(Guid requestId)
        {
            // Fetch the PatientRequest by ID
            Request = await _patientRequestService.GetPatientRequestByIdAsync(requestId);
            if (Request == null)
            {
                return NotFound();
            }

            // Fetch Customers, Doctors, and Services for dropdowns
            Customers = await _userService.GetAllUsersAsync();
            Doctors = Customers.Where(u => u.Role == "Doctor").ToList();
            Customers = Customers.Where(u => u.Role == "Customer").ToList();
            Services = await _treatmentServiceService.GetAllTreatmentServicesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Request.RequestedDate == default(DateTime))
                {
                    Request.RequestedDate = DateTime.Now; // Use current date if not set
                }

                if (Request.CreatedDate == default(DateTime))
                {
                    Request.CreatedDate = DateTime.Now; // Set to current date if not set
                }
                await _patientRequestService.UpdatePatientRequestAsync(Request);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}

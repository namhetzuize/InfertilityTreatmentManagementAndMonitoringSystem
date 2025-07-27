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
        private readonly TreatmentServiceService _treatmentServiceService;
        private readonly UserService _userService;

        public EditModel(PatientRequestService patientRequestService, TreatmentServiceService treatmentServiceService, UserService userService)
        {
            _patientRequestService = patientRequestService;
            _treatmentServiceService = treatmentServiceService;
            _userService = userService;
        }

        public List<User> Customers { get; set; }
        public List<User> Doctors { get; set; }

        [BindProperty]
        public PatientRequest Request { get; set; }

        public List<TreatmentService> Services { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid requestId)
        {
            Request = await _patientRequestService.GetPatientRequestByIdAsync(requestId);
            if (Request == null)
                return NotFound();

            Customers = await _userService.GetAllUsersAsync();
            Doctors = Customers.Where(u => u.Role == "Doctor").ToList();
            Customers = Customers.Where(u => u.Role == "Customer").ToList();

            Services = await _treatmentServiceService.GetAllTreatmentServicesAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                Customers = await _userService.GetAllUsersAsync();
                Doctors = Customers.Where(u => u.Role == "Doctor").ToList();
                Customers = Customers.Where(u => u.Role == "Customer").ToList();
                Services = await _treatmentServiceService.GetAllTreatmentServicesAsync();

                return Page();
            }

            var existingRequest = await _patientRequestService.GetPatientRequestByIdAsync(Request.RequestId);
            if (existingRequest == null)
            {
                ModelState.AddModelError(string.Empty, "Không tìm thấy yêu cầu để cập nhật.");
                return Page();
            }
            existingRequest.Note = Request.Note;
            existingRequest.ServiceId = Request.ServiceId;
            await _patientRequestService.UpdatePatientRequestAsync(existingRequest);

            return RedirectToPage("/PatientRequestPage/Details", new { requestId = Request.RequestId});

        }
    }
}

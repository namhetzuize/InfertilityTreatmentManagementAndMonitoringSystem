using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.SchedulePage
{
    public class CreateModel : PageModel
    {
        private readonly ScheduleService _scheduleService;
        private readonly UserService _userService;
        private readonly TreatmentServiceService _treatmentServiceService;

        [BindProperty]
        public Schedule NewSchedule { get; set; }

        public List<User> Customers { get; set; }
        public List<User> Doctors { get; set; }
        public List<TreatmentService> Services { get; set; }

        public CreateModel(ScheduleService scheduleService, UserService userService, TreatmentServiceService treatmentServiceService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
        }

        public async Task OnGetAsync()
        {
            // Fetch Customers, Doctors, and Services for the dropdowns
            Customers = await _userService.GetAllUsersAsync();
            Doctors = Customers.Where(u => u.Role == "Doctor").ToList();
            Customers = Customers.Where(u => u.Role == "Customer").ToList();
            Services = await _treatmentServiceService.GetAllTreatmentServicesAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                // Check if the service exists by its name
                var service = await _treatmentServiceService.GetServiceByServiceNameAsync(NewSchedule.SerivceName);

                if (service == null)
                {
                    ModelState.AddModelError("SerivceName", "The service does not exist.");
                    return Page(); // Show an error if the service doesn't exist
                }

                // Create the schedule
                service.ServiceName = NewSchedule.SerivceName;
                await _scheduleService.CreateScheduleAsync(NewSchedule);
                return RedirectToPage("./Index");
            }

            return Page(); // Return the page if model state is invalid
        }
    }
}

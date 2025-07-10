using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.SchedulePage
{
    public class EditModel : PageModel
    {
        private readonly ScheduleService _scheduleService;
        private readonly UserService _userService;
        private readonly TreatmentServiceService _treatmentServiceService;

        [BindProperty]
        public Schedule Schedule { get; set; }

        public List<User> Customers { get; set; }
        public List<User> Doctors { get; set; }
        public List<TreatmentService> Services { get; set; }

        public EditModel(ScheduleService scheduleService, UserService userService, TreatmentServiceService treatmentServiceService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
        }

        public async Task<IActionResult> OnGetAsync(Guid scheduleId)
        {
            // Fetch the Schedule by ID
            Schedule = await _scheduleService.GetScheduleByIdAsync(scheduleId);
            if (Schedule == null)
            {
                return NotFound();
            }

            // Fetch Customers, Doctors, and Services for the dropdowns
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
                // Fetch the selected service by its name
                var service = await _treatmentServiceService.GetServiceByServiceNameAsync(Schedule.SerivceName);

                if (service != null)
                {
                    // Since the service is found, update the Schedule with the correct service
                    Schedule.SerivceName = service.ServiceName; // Store the correct ServiceName
                }
                else
                {
                    // Handle the case where the service does not exist (optional)
                    ModelState.AddModelError("", "Selected service does not exist.");
                    return Page();
                }

                // Perform the update
                await _scheduleService.UpdateScheduleAsync(Schedule);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}

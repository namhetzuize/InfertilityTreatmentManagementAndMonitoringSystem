using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.SchedulePage
{
    public class DetailsModel : PageModel
    {
        private readonly ScheduleService _scheduleService;
        private readonly UserService _userService;
        private readonly TreatmentServiceService _treatmentServiceService;

        public Schedule Schedule { get; set; }
        public User Customer { get; set; }
        public User Doctor { get; set; }
        public TreatmentService Service { get; set; }

        public DetailsModel(ScheduleService scheduleService, UserService userService, TreatmentServiceService treatmentServiceService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
        }

        public async Task<IActionResult> OnGetAsync(Guid scheduleId)
        {
            // Fetch the schedule by ID
            Schedule = await _scheduleService.GetScheduleByIdAsync(scheduleId);
            if (Schedule == null)
            {
                return NotFound();
            }

            // Fetch the customer, doctor, and service based on the schedule information
            Customer = await _userService.GetUserByIdAsync(Schedule.CustomerId);
            Doctor = await _userService.GetUserByIdAsync(Schedule.DoctorId);
            return Page();
        }
    }
}

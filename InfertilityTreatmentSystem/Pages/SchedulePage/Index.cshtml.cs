using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.SchedulePage
{
    public class IndexModel : PageModel
    {
        private readonly ScheduleService _scheduleService;
        private readonly UserService _userService;

        public List<Schedule> Schedules { get; set; }

        public IndexModel(ScheduleService scheduleService, UserService userService, TreatmentServiceService treatmentServiceService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
        }

        public async Task OnGetAsync()
        {
            // Fetch all schedules with customer, doctor, and service data
            Schedules = await _scheduleService.GetAllSchedulesAsync();
            foreach (var schedule in Schedules)
            {
                schedule.Customer = await _userService.GetUserByIdAsync(schedule.CustomerId);
                schedule.Doctor = await _userService.GetUserByIdAsync(schedule.DoctorId);
            }
        }
    }
}

using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.SchedulePage
{
    public class IndexModel : PageModel
    {
        private readonly ScheduleService _scheduleService;
        private readonly UserService _userService;

        public IndexModel(ScheduleService scheduleService, UserService userService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid AppointmentId { get; set; }

        public List<Schedule> Schedules { get; set; }

        public async Task OnGetAsync()
        {
            Schedules = await _scheduleService.GetSchedulesByAppointmentIdAsync(AppointmentId);

            // Nếu cần load tên bác sĩ & khách hàng
            foreach (var schedule in Schedules)
            {
                schedule.Doctor = await _userService.GetUserByIdAsync(schedule.DoctorId);
                schedule.Customer = await _userService.GetUserByIdAsync(schedule.CustomerId);
            }
        }
    }
}

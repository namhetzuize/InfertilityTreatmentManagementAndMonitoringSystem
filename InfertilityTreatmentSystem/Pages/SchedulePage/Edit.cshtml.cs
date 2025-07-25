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

        public EditModel(ScheduleService scheduleService, UserService userService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
           
        }

        public async Task<IActionResult> OnGetAsync(Guid scheduleId, Guid appointmentId)
        {
            Schedule = await _scheduleService.GetScheduleByIdAsync(scheduleId);
            if (Schedule == null)
            {
                return NotFound();
            }

            // Lưu lại appointmentId nếu cần redirect về sau
            Schedule.AppointmentId = appointmentId;

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

            await _scheduleService.UpdateScheduleAsync(Schedule);
            Console.WriteLine($"Redirecting with appointmentId: {Schedule.AppointmentId}");
            return RedirectToPage("/SchedulePage/Details", new { scheduleId = Schedule.ScheduleId, appointmentId = Schedule.AppointmentId });
        }
    }
}

using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.SchedulePage
{
    public class DeleteModel : PageModel
    {
        private readonly ScheduleService _scheduleService;

        [BindProperty]
        public Schedule Schedule { get; set; }

        public DeleteModel(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        public async Task<IActionResult> OnGetAsync(Guid scheduleId)
        {
            Schedule = await _scheduleService.GetScheduleByIdAsync(scheduleId);
            if (Schedule == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid scheduleId)
        {
            var schedule = await _scheduleService.GetScheduleByIdAsync(scheduleId);
            if (Schedule != null)
            {
                Guid customerId = schedule.CustomerId;
                Guid doctorId = schedule.DoctorId;
                await _scheduleService.DeleteScheduleByIdAsync(Schedule.ScheduleId);
                return RedirectToPage("/MedicalProfileDetails", new { customerId = customerId, doctorId = doctorId });
            }

            return NotFound();
        }
    }
}

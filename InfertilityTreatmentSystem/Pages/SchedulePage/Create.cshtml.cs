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
        private readonly AppointmentService _appointmentService;

        public CreateModel(ScheduleService scheduleService, AppointmentService appointmentService)
        {
            _scheduleService = scheduleService;
            _appointmentService = appointmentService;
        }

        [BindProperty]
        public Schedule Schedule { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid AppointmentId { get; set; }

        public string CustomerName { get; set; }
        public string DoctorName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(AppointmentId);
            if (appointment == null) return NotFound();

            // Gán tự động
            Schedule = new Schedule
            {
                AppointmentId = AppointmentId,
                CustomerId = appointment.CustomerId,
                DoctorId = appointment.DoctorId,
                SerivceName = appointment.Service?.ServiceName ?? "N/A"
            };

            CustomerName = appointment.Customer?.FullName;
            DoctorName = appointment.Doctor?.FullName;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            if (Schedule.ScheduleDate <= DateTime.Now)
            {
                ModelState.AddModelError("Schedule.ScheduleDate", "Vui lòng chọn thời gian hợp lệ.");
                return Page();
            }

            await _scheduleService.CreateScheduleAsync(Schedule);
            return RedirectToPage("/AppointmentPage/Details", new { id = Schedule.AppointmentId });
        }
    }
}

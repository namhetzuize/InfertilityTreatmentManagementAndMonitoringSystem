using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace InfertilityTreatmentSystem.Pages
{
    [Authorize(Roles = "Doctor")]
    public class DoctorModel : PageModel
    {
        private readonly AppointmentService _appointmentService;
        private readonly ScheduleService _scheduleService;
        private readonly UserService _userService;

        public DoctorModel(AppointmentService appointmentService, ScheduleService scheduleService, UserService userService)
        {
            _appointmentService = appointmentService;
            _scheduleService = scheduleService;
            _userService = userService;


        }

        [BindProperty(SupportsGet = true)]
        public Guid AppointmentId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid DoctorId { get; set; }

        public int TotalAppointments { get; set; }
        public int TotalSchedules { get; set; }
        public string DoctorName { get; set; }
        public List<Appointment> AllAppointments { get; set; } = new List<Appointment>();
        public List<Appointment> UpcomingAppointments { get; set; } = new();


        public async Task OnGetAsync()
        {

            var userIdClaim = User.FindFirst("UserId")?.Value;
            if (!Guid.TryParse(userIdClaim, out Guid doctorId)) return;
            DoctorId = doctorId;

            var doctor = await _userService.GetUserByIdAsync(doctorId);
            DoctorName = doctor?.FullName ?? "Doctor";

            var appointments = await _appointmentService.GetAppointmentsByDoctorIdAsync(doctorId);

            TotalAppointments = appointments.Count;
            AllAppointments = appointments;
            UpcomingAppointments = appointments.FindAll(a => a.AppointmentDate > DateTime.Now);

            var schedules = await _scheduleService.GetSchedulesByAppointmentIdAsync(AppointmentId);
        }
        public async Task<IActionResult> OnPostAsync(Guid appointmentId)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
            if (appointment != null && appointment.Status != "Success")
            {
                appointment.Status = "Success";
                await _appointmentService.UpdateAppointmentAsync(appointment);
            }

            return RedirectToPage(); // refresh
        }
    }
}

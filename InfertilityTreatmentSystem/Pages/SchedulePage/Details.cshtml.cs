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
        private readonly AppointmentService _appointmentService;

        public DetailsModel(ScheduleService scheduleService,
                            UserService userService,
                            TreatmentServiceService treatmentServiceService,
                            AppointmentService appointmentService)
        {
            _scheduleService = scheduleService;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
            _appointmentService = appointmentService;
        }

        public Schedule Schedule { get; set; }
        public User Customer { get; set; }
        public User Doctor { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid ScheduleId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid AppointmentId { get; set; }

        public Guid CustomerId { get; set; }
        public Guid DoctorId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            // Lấy lịch khám
            Schedule = await _scheduleService.GetScheduleByIdAsync(ScheduleId);
            if (Schedule == null)
            {
                return NotFound();
            }

            // Lấy appointment → để truy customerId và doctorId
            var appointment = await _appointmentService.GetAppointmentByIdAsync(AppointmentId);
            if (appointment == null)
            {
                return NotFound();
            }

            CustomerId = appointment.CustomerId;
            DoctorId = appointment.DoctorId;

            // Lấy thông tin người dùng
            Customer = await _userService.GetUserByIdAsync(CustomerId);
            Doctor = await _userService.GetUserByIdAsync(DoctorId);

            return Page();
        }
    }
}

using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.AppointmentPage
{
    public class CreateModel : PageModel
    {
        private readonly AppointmentService _appointmentService;
        private readonly TreatmentServiceService _treatmentServiceService;

        public CreateModel(
            AppointmentService appointmentService,
            TreatmentServiceService treatmentServiceService)
        {
            _appointmentService = appointmentService;
            _treatmentServiceService = treatmentServiceService;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public List<SelectListItem> Doctors { get; set; }
        public List<SelectListItem> Services { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
                return RedirectToPage("/Account/Login"); // or show error

            Appointment = new Appointment
            {
                CustomerId = userId,
                Status = "Pending"
            };
            await PopulateDropdownsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = GetCurrentUserId();
            if (userId == Guid.Empty)
            {
                ModelState.AddModelError("", "Không xác định được tài khoản của bạn. Vui lòng đăng nhập lại.");
                await PopulateDropdownsAsync();
                return Page();
            }

            Appointment.CustomerId = userId;
            Appointment.Status = "Pending";

            // 1) duplicate-pending check
            var existing = await _appointmentService
                .GetAppointmentByCustomerAndDoctorAsync(
                    Appointment.CustomerId,
                    Appointment.DoctorId);
            if (existing != null && existing.Status == "Pending")
            {
                ModelState.AddModelError(
                    "Appointment.DoctorId",
                    "Bạn đã có một lịch chờ xử lý với bác sĩ này. " +
                    "Vui lòng xoá lịch cũ trước khi tạo mới.");
            }

            // 2) future-date check
            if (Appointment.AppointmentDate <= DateTime.Now)
                ModelState.AddModelError(
                    "Appointment.AppointmentDate",
                    "Vui lòng chọn một thời điểm trong tương lai.");

            // 3) office hours
            var hr = Appointment.AppointmentDate.Hour;
            if (hr < 8 || hr >= 17)
                ModelState.AddModelError(
                    "Appointment.AppointmentDate",
                    "Vui lòng chọn giờ trong khoảng từ 8h đến 17h.");

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return Page();
            }

            Appointment.AppointmentId = Guid.NewGuid();
            await _appointmentService.CreateAppointmentAsync(Appointment);
            return RedirectToPage("/Home");
        }

        private async Task PopulateDropdownsAsync()
        {
            // only doctors
            var (_, docs) = await _appointmentService.GetCustomersAndDoctorsAsync();
            Doctors = docs.Select(d =>
                new SelectListItem(d.FullName, d.UserId.ToString()))
                .ToList();

            // services
            var svcs = await _treatmentServiceService.GetAllTreatmentServicesAsync();
            Services = svcs.Select(s =>
                new SelectListItem(s.ServiceName, s.ServiceId.ToString()))
                .ToList();
        }

        private Guid GetCurrentUserId()
        {
            // *** use the exact claim name you issued at login ***
            var claim = User.FindFirst("UserId")?.Value;
            return Guid.TryParse(claim, out var id) ? id : Guid.Empty;
        }
    }
}

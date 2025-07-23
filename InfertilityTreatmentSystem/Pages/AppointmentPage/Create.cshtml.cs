using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.AppointmentPage
{
    public class CreateModel : PageModel
    {
        private readonly AppointmentService _appointmentService;
        private readonly UserService _userService;
        private readonly TreatmentServiceService _treatmentServiceService;

        public CreateModel(
            AppointmentService appointmentService,
            UserService userService,
            TreatmentServiceService treatmentServiceService)
        {
            _appointmentService = appointmentService;
            _userService = userService;
            _treatmentServiceService = treatmentServiceService;
        }

        [BindProperty]
        public Appointment Appointment { get; set; }

        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> Doctors { get; set; }
        public List<SelectListItem> Services { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {

            Appointment = new Appointment
            {
                CustomerId = GetCurrentUserId()  // Tự gán từ claim
            };
            await PopulateDropdownsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Appointment.CustomerId = GetCurrentUserId();
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return Page();
            }

            if (Appointment.AppointmentId == Guid.Empty)
                Appointment.AppointmentId = Guid.NewGuid();
            var selectedDateTime = Appointment.AppointmentDate;
           
            if (selectedDateTime <= DateTime.Now)
            {
                ModelState.AddModelError("Appointment.AppointmentDate", "Vui lòng chọn giờ hợp lí.");
            }

            if (selectedDateTime.Hour < 8 || selectedDateTime.Hour >= 17)
            {
                ModelState.AddModelError("Appointment.AppointmentDate", "Vui lòng chọn giờ trong khoảng từ 8h đến 17h.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return Page();
            }
            Appointment.Status = "Pending";


            await _appointmentService.CreateAppointmentAsync(Appointment);
            return RedirectToPage("/Home");
        }

        private async Task PopulateDropdownsAsync()
        {
            // load customers & doctors
            var (custs, docs) = await _appointmentService.GetCustomersAndDoctorsAsync();
            Customers = custs
                .Select(u => new SelectListItem(u.FullName, u.UserId.ToString()))
                .ToList();
            Doctors = docs
                .Select(u => new SelectListItem(u.FullName, u.UserId.ToString()))
                .ToList();

            // load services via your TreatmentServiceService
            var svcs = await _treatmentServiceService.GetAllTreatmentServicesAsync();
            Services = svcs
                .Select(s => new SelectListItem(s.ServiceName, s.ServiceId.ToString()))
                .ToList();
        }

        private Guid GetCurrentUserId()
        {
            var claim = User.FindFirst("UserId");
            return claim != null ? Guid.Parse(claim.Value) : Guid.Empty;
        }
    }
}

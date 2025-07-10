using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.AppointmentPage
{
    public class EditModel : PageModel
    {
        private readonly AppointmentService _appointmentService;
        private readonly UserService _userService;
        private readonly TreatmentServiceService _treatmentServiceService;

        public EditModel(
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

        // The ID parameter from the URL
        public Guid Id { get; set; }

        // OnGetAsync method to fetch the appointment and populate dropdowns
        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            // Ensure the id is provided in the URL
            if (id == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Appointment ID is missing.");
                return RedirectToPage("./Index");
            }

            // Fetch the appointment using the provided id
            Appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (Appointment == null)
            {
                // If the appointment is not found
                return NotFound();
            }

            // Store the id in the model for later use (e.g., in OnPostAsync)
            Id = id;

            // Populate dropdowns for Customers, Doctors, and Services
            await PopulateDropdownsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Ensure the AppointmentId is not empty before performing the update
            if (Appointment.AppointmentId == Guid.Empty)
            {
                ModelState.AddModelError(string.Empty, "Appointment ID is missing or invalid.");
                return Page();
            }

            // If model validation fails, repopulate dropdowns and return to the page
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return Page();
            }

            // Perform the update
            await _appointmentService.UpdateAppointmentAsync(Appointment);

            // Redirect to the index page after successful update
            return RedirectToPage("./Index");
        }

        // Helper method to populate the dropdowns for Customers, Doctors, and Services
        private async Task PopulateDropdownsAsync()
        {
            // Fetch Customers and Doctors from the service
            var (custs, docs) = await _appointmentService.GetCustomersAndDoctorsAsync();
            Customers = custs.Select(u => new SelectListItem(u.UserName, u.UserId.ToString(), u.UserId == Appointment.CustomerId)).ToList();
            Doctors = docs.Select(u => new SelectListItem(u.UserName, u.UserId.ToString(), u.UserId == Appointment.DoctorId)).ToList();

            // Fetch Treatment Services for the Service dropdown
            var svcs = await _treatmentServiceService.GetAllTreatmentServicesAsync();
            Services = svcs.Select(s => new SelectListItem(s.ServiceName, s.ServiceId.ToString(), s.ServiceId == Appointment.ServiceId)).ToList();
        }
    }
}

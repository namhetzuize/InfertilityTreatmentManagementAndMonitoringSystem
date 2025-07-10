using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.AppointmentPage
{
    public class DeleteModel : PageModel
    {
        private readonly AppointmentService _appointmentService;

        // The appointment object to be deleted
        [BindProperty]
        public Appointment Appointment { get; set; }

        public DeleteModel(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // OnGetAsync method loads the appointment data to confirm before deleting
        public async Task<IActionResult> OnGetAsync(Guid appointmentId)
        {
            // Get the appointment details using its ID
            Appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);

            if (Appointment == null)
            {
                return NotFound(); // If appointment is not found, return NotFound()
            }

            return Page(); // Return the page to show the delete confirmation
        }

        // OnPostAsync handles the deletion when the user confirms the delete action
        public async Task<IActionResult> OnPostAsync()
        {
            if (Appointment != null)
            {
                // Delete the appointment by ID using DeleteAppointmentByIdAsync
                await _appointmentService.DeleteAppointmentByIdAsync(Appointment.AppointmentId);
                return RedirectToPage("/AppointmentPage/Index"); // Redirect to the appointment list page after deletion
            }

            return NotFound(); // If appointment is null, return NotFound
        }
    }
}

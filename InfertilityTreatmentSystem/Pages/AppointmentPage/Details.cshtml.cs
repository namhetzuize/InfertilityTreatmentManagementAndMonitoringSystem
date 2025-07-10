using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.AppointmentPage
{
    public class DetailsModel : PageModel
    {
        private readonly AppointmentService _appointmentService;

        public DetailsModel(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public Appointment Appointment { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (Appointment == null) return NotFound();
            return Page();
        }
    }
}

using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages.AppointmentPage
{
    public class IndexModel : PageModel
    {
        private readonly AppointmentService _appointmentService;

        public IndexModel(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public List<Appointment> Appointments { get; set; }

        public async Task OnGetAsync()
        {
            // Fetch all appointments with Customer and Doctor info loaded
            Appointments = await _appointmentService.GetAllAppointmentsAsync();
        }
    }
}

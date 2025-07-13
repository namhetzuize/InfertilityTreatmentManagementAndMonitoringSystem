using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

 using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace InfertilityTreatmentSystem.Pages.AppointmentPage
{
    [Authorize]
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
            var userIdStr = User.FindFirst("UserId")?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            Console.WriteLine($"UserId: {userIdStr}");
            Console.WriteLine($"Role: {role}");

            if (!Guid.TryParse(userIdStr, out Guid userId))
            {
                Appointments = new List<Appointment>(); 
                return;
            }

            if (role == "Admin")
            {
                Appointments = await _appointmentService.GetAllAppointmentsAsync();
            }
            else if (role == "Customer")
            {
                Appointments = await _appointmentService.GetAppointmentsByCustomerIdAsync(userId);
            }
            else if (role == "Doctor")
            {
                Appointments = await _appointmentService.GetAppointmentsByDoctorIdAsync(userId);
            }
            else
            {
                Appointments = new List<Appointment>();
            }
        }
    }
}

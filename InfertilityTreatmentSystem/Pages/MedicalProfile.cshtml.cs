using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;

namespace InfertilityTreatmentSystem.Pages
{
    [Authorize(Roles = "Doctor")]
    public class MedicalProfileModel : PageModel
    {
    private readonly AppointmentService _appointmentService;

    public MedicalProfileModel(AppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public List<Appointment> MedicalProfiles { get; set; }

    public async Task OnGetAsync()
    {
        var doctorIdStr = User.FindFirst("UserId")?.Value;
        if (!Guid.TryParse(doctorIdStr, out var doctorId))
        {
            MedicalProfiles = new List<Appointment>();
            return;
        }

        var allAppointments = await _appointmentService.GetAppointmentsByDoctorIdAsync(doctorId);
        MedicalProfiles = allAppointments
            .Where(a => a.Status == "Success")
            .ToList();
    }
}
}

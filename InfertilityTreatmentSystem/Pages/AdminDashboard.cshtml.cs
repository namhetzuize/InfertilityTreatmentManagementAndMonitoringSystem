using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace InfertilityTreatmentSystem.Pages
{
    [Authorize(Roles = "Admin")]
    public class AdminDashboardModel : PageModel
    {
        private readonly UserService _userService;
        private readonly AppointmentService _appointmentService;
        private readonly BlogService _blogService;
        private readonly PatientRequestService _patientRequestService;
        private readonly TreatmentServiceService _treatmentServiceService;

        public int TotalUsers { get; private set; }
        public int TotalDoctors { get; private set; }
        public int TotalCustomers { get; private set; }
        public int TotalAppointments { get; private set; }
        public int TotalBlogs { get; private set; }
        public int TotalRequests { get; private set; }
        public int TotalServices { get; private set; }

        public AdminDashboardModel(
            UserService userService,
            AppointmentService appointmentService,
            BlogService blogService,
            PatientRequestService patientRequestService,
            TreatmentServiceService treatmentServiceService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
            _blogService = blogService;
            _patientRequestService = patientRequestService;
            _treatmentServiceService = treatmentServiceService;
        }

        public async Task OnGetAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            TotalUsers = users.Count;
            TotalDoctors = users.Count(u => u.Role == "Doctor");
            TotalCustomers = users.Count(u => u.Role == "Customer");

            var appts = await _appointmentService.GetAllAppointmentsAsync();
            TotalAppointments = appts.Count;

            var blogs = await _blogService.GetAllBlogsAsync();
            TotalBlogs = blogs.Count;

           

            var svcs = await _treatmentServiceService.GetAllTreatmentServicesAsync();
            TotalServices = svcs.Count;
        }
    }
}

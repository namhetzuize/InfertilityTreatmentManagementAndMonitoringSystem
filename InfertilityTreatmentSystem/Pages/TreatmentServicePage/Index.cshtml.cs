using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.TreatmentServicePage
{
    public class IndexModel : PageModel
    {
        private readonly TreatmentServiceService _treatmentServiceService;
        private readonly UserService _userService; // Added UserService to get the user info

        public List<TreatmentService> TreatmentServices { get; set; }

        public IndexModel(TreatmentServiceService treatmentServiceService, UserService userService)
        {
            _treatmentServiceService = treatmentServiceService;
            _userService = userService;
        }

        public async Task OnGetAsync()
        {
            // Fetch all treatment services
            TreatmentServices = await _treatmentServiceService.GetAllTreatmentServicesAsync();

            // Fetch the user associated with each treatment service
            foreach (var service in TreatmentServices)
            {
                service.User = await _userService.GetUserByIdAsync(service.UserId);
            }
        }
    }
}

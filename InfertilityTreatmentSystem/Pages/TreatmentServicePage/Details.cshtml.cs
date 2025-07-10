using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.TreatmentServicePage
{
    public class DetailsModel : PageModel
    {
        private readonly TreatmentServiceService _treatmentServiceService;
        private readonly UserService _userService; // We will use this service to get the user details

        public TreatmentService Service { get; set; }
        public User ServiceCreator { get; set; }  // User who created this treatment service

        public DetailsModel(TreatmentServiceService treatmentServiceService, UserService userService)
        {
            _treatmentServiceService = treatmentServiceService;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync(Guid serviceId)
        {
            // Retrieve the treatment service by its ID
            Service = await _treatmentServiceService.GetTreatmentServiceByIdAsync(serviceId);

            if (Service == null)
            {
                return NotFound(); // If service not found, return 404
            }

            // Now retrieve the User who created this service (based on UserId)
            ServiceCreator = await _userService.GetUserByIdAsync(Service.UserId);

            if (ServiceCreator == null)
            {
                return NotFound(); // If user not found, return 404
            }

            return Page(); // If everything is fine, render the page
        }
    }
}

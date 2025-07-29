using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace InfertilityTreatmentSystem.Pages.TreatmentServicePage
{
    [Authorize(Roles = "Admin,Doctor")]
    public class CreateModel : PageModel
    {
        private readonly TreatmentServiceService _treatmentServiceService;

        public CreateModel(TreatmentServiceService treatmentServiceService)
        {
            _treatmentServiceService = treatmentServiceService;
        }

        [BindProperty]
        public TreatmentService TreatmentService { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var userIdStr = User.FindFirstValue("UserId");
            if (!Guid.TryParse(userIdStr, out Guid userId))
                return Unauthorized();

            TreatmentService.ServiceId = Guid.NewGuid(); // Tự sinh
            TreatmentService.UserId = userId;

            await _treatmentServiceService.CreateTreatmentServiceAsync(TreatmentService);

            return RedirectToPage("./Index");
        }
    }
}

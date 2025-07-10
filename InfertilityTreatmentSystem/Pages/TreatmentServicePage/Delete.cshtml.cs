using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.TreatmentServicePage
{
    public class DeleteModel : PageModel
    {
        private readonly TreatmentServiceService _treatmentServiceService;

        [BindProperty]
        public TreatmentService Service { get; set; }

        public DeleteModel(TreatmentServiceService treatmentServiceService)
        {
            _treatmentServiceService = treatmentServiceService;
        }

        public async Task<IActionResult> OnGetAsync(Guid serviceId)
        {
            Service = await _treatmentServiceService.GetTreatmentServiceByIdAsync(serviceId);

            if (Service == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Service != null)
            {
                await _treatmentServiceService.DeleteTreatmentServiceByIdAsync(Service.ServiceId);
                return RedirectToPage("./Index");
            }

            return NotFound();
        }
    }
}

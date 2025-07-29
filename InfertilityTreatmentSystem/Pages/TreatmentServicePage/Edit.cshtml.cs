using InfertilityTreatmentSystem.BLL.Service;
using InfertilityTreatmentSystem.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InfertilityTreatmentSystem.Pages.TreatmentServicePage
{
    public class EditModel : PageModel
    {
        private readonly TreatmentServiceService _treatmentServiceService;

        [BindProperty]
        public TreatmentService Service { get; set; }

        public EditModel(TreatmentServiceService treatmentServiceService)
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

        public async Task<IActionResult> OnPostAsync(Guid serviceId)
        {
            if (ModelState.IsValid)
            {
                if(serviceId == null)
                {
                    return NotFound();
                }
                await _treatmentServiceService.UpdateTreatmentServiceByIdAsync(serviceId, Service);
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}

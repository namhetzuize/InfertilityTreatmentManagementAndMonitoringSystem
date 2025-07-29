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

        public async Task<IActionResult> OnPostAsync(Guid serviceId)
        {
            var service = await _treatmentServiceService.GetByIdWithRelationsAsync(serviceId);
            if (service == null) return NotFound();

            if (service.Appointments.Any() || service.PatientRequests.Any())
            {
                TempData["ErrorMessage"] = "Không thể xoá dịch vụ này vì đã được sử dụng trong lịch hẹn hoặc yêu cầu điều trị.";
                return RedirectToPage("./Details", new { serviceId });
            }

            await _treatmentServiceService.DeleteTreatmentServiceByIdAsync(service.ServiceId);
            return RedirectToPage("./Index");
        }
    }
}

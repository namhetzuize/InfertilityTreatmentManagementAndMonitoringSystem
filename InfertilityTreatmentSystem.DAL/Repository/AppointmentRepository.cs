using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.Data.Base;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>
    {
        public AppointmentRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }
    }
}

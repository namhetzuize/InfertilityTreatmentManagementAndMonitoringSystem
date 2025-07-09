using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.Data.Base;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class PatientRequestRepository : GenericRepository<PatientRequest>
    {
        public PatientRequestRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }
    }
}

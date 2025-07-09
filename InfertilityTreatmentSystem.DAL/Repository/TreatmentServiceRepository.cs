using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.Data.Base;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class TreatmentServiceRepository : GenericRepository<TreatmentService>
    {
        public TreatmentServiceRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }
    }
}

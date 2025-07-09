using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.Data.Base;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class MedicalRecordRepository : GenericRepository<MedicalRecord>
    {
        public MedicalRecordRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }
    }
}

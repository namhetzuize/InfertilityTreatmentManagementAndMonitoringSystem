using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.Data.Base;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class ScheduleRepository: GenericRepository<Schedule>
    {
        public ScheduleRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }
    }
}

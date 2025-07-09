using InfertilityTreatmentSystem.DAL.Models;
using InfertilityTreatmentSystem.Data.Base;

namespace InfertilityTreatmentSystem.DAL.Repository
{
    public class BlogRepository : GenericRepository<Blog>
    {
        public BlogRepository(InfertilityTreatmentDBContext context) : base(context)
        {
        }
    }
}

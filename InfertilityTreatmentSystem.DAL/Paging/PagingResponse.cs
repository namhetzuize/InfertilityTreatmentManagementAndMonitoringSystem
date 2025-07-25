namespace InfertilityTreatmentSystem.DAL.Paging
{
    public class PagingResponse<T>
    {
        public List<T> List { get; set; }
        public int TotalPages { get; set; }
        public int PageIndex { get; set; }

        public PagingResponse()
        {
            List = new List<T>();
        }
    }
}

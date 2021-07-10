namespace Application.Features.CustomerFeatures.Queries.GetAll
{
    public class GetAllCustomersResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
    }
}
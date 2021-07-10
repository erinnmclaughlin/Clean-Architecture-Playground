namespace Application.Features.CustomerFeatures.Queries.GetById
{
    public class GetCustomerByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }
    }
}
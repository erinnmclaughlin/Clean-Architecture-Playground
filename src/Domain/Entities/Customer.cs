using Domain.Common;

namespace Domain.Entities
{
    public class Customer : EntityBase<int>
    {
        public string Name { get; set; }
        public string Location { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }
    }
}
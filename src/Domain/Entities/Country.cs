using Domain.Common;

namespace Domain.Entities
{
    public class Country : EntityBase<int>
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
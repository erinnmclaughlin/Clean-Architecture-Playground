using Domain.Entities;

namespace Application.Filters
{
    public class CustomerFilter : FilterBase<Customer>
    {
        public CustomerFilter(string searchString)
        {
            Includes.Add(x => x.Country);

            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = x => x.Country != null && (x.Location.Contains(searchString) || x.Name.Contains(searchString));
            }
            else
            {
                Criteria = x => x.Country != null;
            }
        }
    }
}
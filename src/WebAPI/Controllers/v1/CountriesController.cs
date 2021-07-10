using Application.Features.CountryFeatures.Queries.GetAll;
using Application.Features.CountryFeatures.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers.v1
{
    public class CountriesController : ApiControllerBase
    {
        public CountriesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var countries = await Mediator.Send(new GetAllCountriesQuery());
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var country = await Mediator.Send(new GetCountryByIdQuery { Id = id });
            return Ok(country);
        }
    }
}

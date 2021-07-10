using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IMediator Mediator;

        public ApiControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
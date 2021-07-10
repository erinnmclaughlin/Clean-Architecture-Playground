using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator;

        public BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
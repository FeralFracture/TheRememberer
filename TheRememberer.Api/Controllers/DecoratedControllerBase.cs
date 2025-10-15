using Microsoft.AspNetCore.Mvc;

namespace TheRememberer.Api.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/[controller]")]
    public abstract class DecoratedControllerBase<T> : ControllerBase
    {
        private readonly ILogger<T> _logger;

        public DecoratedControllerBase(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}

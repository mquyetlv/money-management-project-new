using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace money_management_service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}

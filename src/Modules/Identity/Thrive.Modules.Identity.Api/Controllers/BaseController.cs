using Microsoft.AspNetCore.Mvc;

namespace Thrive.Modules.Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase;
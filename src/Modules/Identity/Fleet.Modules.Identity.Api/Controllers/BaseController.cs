using Microsoft.AspNetCore.Mvc;

namespace Fleet.Modules.Identity.Api.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase;
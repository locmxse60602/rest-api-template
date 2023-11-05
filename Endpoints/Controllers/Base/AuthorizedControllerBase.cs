using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Endpoints.Controllers;

[Authorize]
public class AuthorizedControllerBase : ControllerBase
{
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmRealm.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class SampleController: ControllerBase
{
    [HttpGet]
    public ActionResult<string> Test()
    {
        return Ok("Something");
    }
}
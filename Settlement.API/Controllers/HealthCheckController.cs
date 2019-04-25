using Microsoft.AspNetCore.Mvc;

namespace Settlement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HealthCheckController : ControllerBase
	{
		[HttpGet]
		[Route("Check")]
		public ActionResult Check()
		{
			return Ok();
		}
	}
}
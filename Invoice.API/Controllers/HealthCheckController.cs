using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers
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
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Settlement.API.Application.Commands;
using System.Threading.Tasks;

namespace Settlement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PayerController : ControllerBase
	{
		private readonly IMediator _mediator;

		public PayerController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("CreatePayer")]
		public async Task<IActionResult> CreatePayer(string name)
		{
			var cmd = new CreatePayerCommand()
			{
				Name = name
			};

			var result = await _mediator.Send(cmd);

			return Ok();
		}
	}
}
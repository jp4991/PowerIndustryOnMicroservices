using System;
using System.Threading.Tasks;
using Invoice.API.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Invoice.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InvoiceController : ControllerBase
	{
		private readonly IMediator _mediator;

		public InvoiceController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("CreateInvoice")]
		public async Task<IActionResult> CreateInvoice(Guid payerId, DateTime start, DateTime end)
		{
			var cmd = new CreateInvoiceCommand()
			{
				PayerId = payerId,
				StartPeriod = start,
				EndPeriod = end
			};

			var result = await _mediator.Send(cmd);

			return Ok(result);
		}
	}
}
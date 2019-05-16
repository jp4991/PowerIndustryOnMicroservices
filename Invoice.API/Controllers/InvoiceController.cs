using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Invoice.API.Application.Command;
using Invoice.API.Application.Queries;
using Invoice.API.Dto;
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

		[HttpGet]
		[Route("GetInvoices")]
		public async Task<List<InvoiceDto>> GetInvoices([FromQuery]Guid payerId)
		{
			var query = new GetAllInvoicesQuery()
			{
				PayerId = payerId
			};

			var result = await _mediator.Send(query);
			return result;
		}
	}
}
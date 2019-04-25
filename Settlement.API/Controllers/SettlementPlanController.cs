using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Settlement.API.Application.Commands;
using Settlement.API.Application.Queries;

namespace Settlement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SettlementPlanController : ControllerBase
	{
		private readonly IMediator _mediator;

		public SettlementPlanController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		[Route("CreateSettlementPlan")]
		public async Task<IActionResult> CreateSettlementPlan(Guid payerId)
		{
			var cmd = new CreateSettlementPlanCommand(payerId, Guid.NewGuid())
			{
				PayerId = payerId
			};

			var result = await _mediator.Send(cmd);

			return Ok();
		}

		[HttpGet]
		[Route("GetSettlementPlan")]
		public async Task<IAsyncResult> GetSettlementPlan(Guid id)
		{
			var cmd = new GetSettlementPlanQuery
			{
				SettlementPlanId = id
			};

			return _mediator.Send(cmd);
		}
	}
}
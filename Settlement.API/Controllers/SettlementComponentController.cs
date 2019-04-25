using MediatR;
using Microsoft.AspNetCore.Mvc;
using Settlement.API.Application.Commands;
using Settlement.API.Dto;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Settlement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SettlementComponentController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly ISettlementComponentRepository _settlementComponentRepository;


		public SettlementComponentController(IMediator mediator,
			ISettlementComponentRepository settlementComponentRepository)
		{
			_mediator = mediator;
			_settlementComponentRepository = settlementComponentRepository;
		}

		[HttpGet]
		[Route("CreateSettlementComponent")]
		public async Task<IActionResult> CreateSettlementComponent(string name, decimal unitPrice)
		{
			var cmd = new CreateSettlementComponentCommand()
			{
				Name = name,
				UnitPrice = unitPrice
			};

			var result = await _mediator.Send(cmd);

			return Ok();
		}

		[HttpGet]
		[Route("GetAllSettlementComponents")]
		public Task<List<SettlementComponentDto>> GetAllSettlementComponents()
		{
			var components = _settlementComponentRepository.GetAll()
				.Select(x => new SettlementComponentDto() { Id = x.Id, Name = x.Name})
				.ToList();
			return Task.FromResult(components);
		}
	}
}
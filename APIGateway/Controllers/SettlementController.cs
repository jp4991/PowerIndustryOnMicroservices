using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIGateway.Command;
using APIGateway.Dto;
using APIGateway.Dto.Settlement;
using APIGateway.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RawRabbit;
using RestEase;

namespace APIGateway.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SettlementController : ControllerBase
	{
		private readonly IBusClient _client;
		private readonly ISettlementService _microservice;

		public SettlementController(IBusClient client, IConfiguration configuration)
		{
			_client = client;
			var host = configuration.GetSection("LoadBalancerAddress").Value;
			_microservice = RestClient.For<ISettlementService>(host);
		}

		//Comands

		[HttpGet]
		[Route("CreateSettlementComponent")]
		public IActionResult CreateSettlementComponent([FromQuery]string name, [FromQuery]decimal unitPrice)
		{
			var cmd = new CreateSettlementComponentCommand(name, unitPrice, Guid.NewGuid());
			_client.PublishAsync(cmd, CFG => CFG.UsePublishConfiguration(C => C.OnExchange("")
				.WithRoutingKey("createsettlementcomponent")));

			return Ok();
		}

		[HttpGet]
		[Route("CreatePayer")]
		public IActionResult CreatePayer([FromQuery]string name)
		{
			var cmd = new CreatePayerCommand(name, Guid.NewGuid());
			_client.PublishAsync(cmd, CFG => CFG.UsePublishConfiguration(C => C.OnExchange("")
				.WithRoutingKey("createpayer")));

			return Ok();
		}

		[HttpGet]
		[Route("CreateSettlementPlan")]
		public IActionResult CreateSettlementPlan([FromQuery]Guid payerId)
		{
			var cmd = new CreateSettlementPlanCommand(payerId, Guid.NewGuid());
			_client.PublishAsync(cmd, CFG => CFG.UsePublishConfiguration(C => C.OnExchange("")
				.WithRoutingKey("createsettlementplan")));

			return Ok();
		}

		//Queries
		[HttpGet]
		[Route("GetAllSettlementComponents")]
		public async Task<List<SettlementComponentDto>> GetAllSettlementComponents()
		{
			return await _microservice.GetAllSettlementComponentsAsync();
		}

		[HttpGet]
		[Route("GetSettlementPlan")]
		public async Task<SettlementPlanDto> GetSettlementPlan([FromQuery]Guid id)
		{
			return await _microservice.GetSettlementPlan(id);
		}

		[HttpGet]
		[Route("GetAllPayers")]
		public async Task<List<PayerDto>> GetAllPayers()
		{
			return await _microservice.GetAllPayers();
		}
	}
}
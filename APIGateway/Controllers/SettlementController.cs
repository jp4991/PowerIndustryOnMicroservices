using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIGateway.Command;
using APIGateway.Dto;
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

		[HttpGet]
		[Route("CreateSettlementComponent")]
		public Task CreateSettlementComponent([FromQuery]string name, [FromQuery]decimal unitPrice)
		{
			var cmd = new CreateSettlementComponentCommand(name, unitPrice, Guid.NewGuid());
			_client.PublishAsync(cmd, CFG => CFG.UsePublishConfiguration(C => C.OnExchange("")
				.WithRoutingKey("createsettlementcomponent")));

			return Task.FromResult(Ok());
		}

		[HttpGet]
		[Route("CreatePayer")]
		public Task CreatePayer([FromQuery]string name)
		{
			var cmd = new CreatePayerCommand(name, Guid.NewGuid());
			_client.PublishAsync(cmd, CFG => CFG.UsePublishConfiguration(C => C.OnExchange("")
				.WithRoutingKey("createpayer")));

			return Task.FromResult(Ok());
		}

		[HttpGet]
		[Route("CreateSettlementPlan")]
		public Task CreateSettlementPlan([FromQuery]Guid payerId)
		{
			var cmd = new CreateSettlementPlanCommand(payerId, Guid.NewGuid());
			_client.PublishAsync(cmd, CFG => CFG.UsePublishConfiguration(C => C.OnExchange("")
				.WithRoutingKey("createsettlementplan")));

			return Task.FromResult(Ok());
		}

		[HttpGet]
		[Route("GetAllSettlementComponents")]
		public async Task<List<SettlementComponentDto>> GetAllSettlementComponents()
		{
			return await _microservice.GetAllSettlementComponentsAsync();
		}
	}
}
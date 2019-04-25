using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIGateway.Services;
using Consul;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using RestEase;

namespace APIGateway.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{

		private readonly IBusClient _rabbitMqbusClient;
		private readonly ISettlementService _microservice;

		public ValuesController(IBusClient client)
		{
			_rabbitMqbusClient = client;

			var host = $@"http://localhost:9999";
			_microservice = RestClient.For<ISettlementService>(host);
		}

		// GET api/values
		[HttpGet]
		public async Task<IEnumerable<string>> Get()
		{
			var result = await _microservice.GetValuesAsync();

			return result;
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public ActionResult<string> Get(int id)
		{
			return "value";
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}

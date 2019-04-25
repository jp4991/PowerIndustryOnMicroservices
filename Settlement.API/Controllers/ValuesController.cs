using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;

namespace Settlement.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly IBusClient _client;

		public ValuesController(IBusClient client)
		{
			_client = client;
		}


		// GET api/values
		[HttpGet]
		public ActionResult<IEnumerable<string>> Get()
		{
			//var cmd = new CreateSettlementComponent
			//{
			//	Name = "Jarek!",
			//	UnitPrice = 90.5M
			//};
			//_client.PublishAsync(cmd, configuration: cfg => cfg.WithRoutingKey("myqueue"));
			//var cmd = new Command1() { Name = "sfsdfsdf" };
			//var t = _client.PublishAsync(cmd, configuration: cfg => cfg.WithRoutingKey("myqueue"));
			return new string[] { "Settlement", "Service" };
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

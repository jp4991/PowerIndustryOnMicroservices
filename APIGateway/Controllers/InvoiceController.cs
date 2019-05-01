using APIGateway.Command;
using Microsoft.AspNetCore.Mvc;
using RawRabbit;
using System;

namespace APIGateway.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
		private readonly IBusClient _rabbitMqbusClient;

		public InvoiceController(IBusClient rabbitMqbusClient)
		{
			_rabbitMqbusClient = rabbitMqbusClient;
		}

		[HttpGet]
		[Route("CreateInvoice")]
		public IActionResult CreateInvoice(Guid payerId,
			DateTime startPeriod,
			DateTime endPeriod)
		{
			var cmd = new CreateInvoiceCommand()
			{
				PayerId = payerId,
				StartPeriod = startPeriod,
				EndPeriod = endPeriod
			};
			_rabbitMqbusClient.PublishAsync(cmd, CFG => CFG.UsePublishConfiguration(C => C.OnExchange("")
				.WithRoutingKey("createinvoice")));
			return Ok();
		}
	}
}
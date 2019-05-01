using MediatR;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using System;

namespace Invoice.API.Application.Command
{
	public class CreateInvoiceCommand : ICommand, IRequest<CreateInvoiceResponse>
	{
		public Guid PayerId { get; set; }
		public DateTime StartPeriod { get; set; }
		public DateTime EndPeriod { get; set; }
		public Guid CorrelationId { get; set; }
	}

	public class CreateInvoiceResponse
	{
		public bool Success { get; set; }
		public Guid? InvoiceId { get; set; }
		public string ErrorDescription { get; set; }
	}
}

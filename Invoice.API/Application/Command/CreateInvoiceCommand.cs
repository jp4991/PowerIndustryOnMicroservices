using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.API.Application.Command
{
	public class CreateInvoiceCommand : IRequest<CreateInvoiceResponse>
	{
		public Guid PayerId { get; set; }
		public DateTime StartPeriod { get; set; }
		public DateTime EndPeriod { get; set; }
	}

	public class CreateInvoiceResponse
	{
		public bool Success { get; set; }
		public Guid? InvoiceId { get; set; }
		public string ErrorDescription { get; set; }
	}
}

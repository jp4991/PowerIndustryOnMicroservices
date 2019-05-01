using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGateway.Command
{
	public class CreateInvoiceCommand
	{
		public Guid PayerId { get; set; }
		public DateTime StartPeriod { get; set; }
		public DateTime EndPeriod { get; set; }
		public Guid CorrelationId { get; set; }
	}
}

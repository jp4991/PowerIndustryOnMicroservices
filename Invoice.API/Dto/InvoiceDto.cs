using Invoice.Domain.AggregateModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.API.Dto
{
	public class InvoiceDto
	{
		public Guid Id { get; set; }
		public Guid PayerId { get; set; }
		public decimal GrossValue { get; set; }
		public decimal NetValue { get; set; }
		public InvoiceStatus Status { get; set; }
		public IList<InvoiceComponentDto> Components { get; set; }
	}
}

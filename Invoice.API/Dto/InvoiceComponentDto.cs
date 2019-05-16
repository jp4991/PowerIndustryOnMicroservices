using System;

namespace Invoice.API.Dto
{
	public class InvoiceComponentDto
	{
		public Guid SettlementComponentId { get; set; }
		public decimal Quantity { get; set; }
		public decimal GrossValue { get; set; }
		public decimal NetValue { get; set; }
		public DateTime StartPeriod { get; set; }
		public DateTime EndPeriod { get; set; }
	}
}

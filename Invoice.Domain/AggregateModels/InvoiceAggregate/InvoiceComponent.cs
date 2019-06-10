using System;

namespace Invoice.Domain.AggregateModels
{
	//VauleObject
	public class InvoiceComponent
	{
		public Guid SettlementComponentId { get; private set; }
		public decimal Quantity { get; private set; }
		public decimal GrossValue { get; private set; }
		public decimal NetValue { get; private set; }
		public DateTime StartPeriod { get; set; }
		public DateTime EndPeriod { get; set; }

		public InvoiceComponent(Guid settlementComponentId,
			decimal quantity,
			decimal unitPrice,
			DateTime startPeriod,
			DateTime endPeriod)
		{
			SettlementComponentId = settlementComponentId;
			Quantity = quantity;
			GrossValue = quantity * unitPrice;
			NetValue = GrossValue * 0.77M;
			StartPeriod = startPeriod;
			EndPeriod = endPeriod;
		}
	}
}

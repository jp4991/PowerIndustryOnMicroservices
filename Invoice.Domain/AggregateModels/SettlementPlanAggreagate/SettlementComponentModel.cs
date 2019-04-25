using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain.AggregateModels.SettlementPlanAggreagate
{
	public class SettlementComponentModel
	{
		public DateTime StartPeriod { get; set; }
		public DateTime EndPeriod { get; set; }
		public decimal Price { get; set; }
		public Guid SettlementComponentId { get; set; }
	}
}

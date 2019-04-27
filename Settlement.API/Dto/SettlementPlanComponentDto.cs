using System;

namespace Settlement.API.Dto
{
	public class SettlementPlanComponentDto
	{
		public Guid SettlementComponentId { get; set; }
		public string SettlementComponentName { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
	}
}

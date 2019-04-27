using System;
using System.Collections.Generic;

namespace APIGateway.Dto.Settlement
{
	public class SettlementPlanDto
	{
		public Guid Id { get; set; }
		public Guid PayerId { get; set; }
		public List<SettlementPlanComponentDto> Components { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain.AggregateModels.SettlementPlanAggreagate
{
	public interface ISettlementComponentRespository
	{
		List<SettlementComponentModel> GetSettlementComponentModelList(DateTime startPeriod, DateTime endPeriod, Guid payerId);
	}
}

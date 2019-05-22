using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Test
{
	public class SettlementComponentModelFactory
	{
		public static List<SettlementComponentModel> CreateModels()
		{
			var settlementComponent1 = new Guid();
			var settlementComponent2 = new Guid();

			List<SettlementComponentModel> models = new List<SettlementComponentModel>();
			for (int i = 1; i <= 12; i++)
			{
				var dayInMonth = DateTime.DaysInMonth(2019, i);
				var start = new DateTime(2019, i, 1);
				var end = new DateTime(2019, i, dayInMonth);
				models.Add(new SettlementComponentModel { StartPeriod = start, EndPeriod = end, Price = 10M, SettlementComponentId = settlementComponent1 });
				models.Add(new SettlementComponentModel { StartPeriod = start, EndPeriod = end, Price = 10M, SettlementComponentId = settlementComponent2 });
			}

			return models;
		}
	}
}

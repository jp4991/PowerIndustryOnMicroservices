using Settlement.Domain.AggregateModels.PayerAggregate;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using Settlement.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Settlement.Domain.AggregateModels.SettlementPlanAggregate
{
	public class SettlementPlan : IAggregateRoot
	{
		public IList<SettlementPlanComponent> SettlementPlanComponents { get; private set; }
		public Guid Id { get; private set; }
		public Guid PayerId { get; private set; }
		public Payer Payer { get; private set; }

		public SettlementPlan()
		{

		}

		public SettlementPlan(Payer payer, IList<SettlementComponent> settlementComponents)
		{
			Payer = payer;
			PayerId = payer.Id;
			SettlementPlanComponents = CreateSettlementPlanComponents(settlementComponents);
		}

		public SettlementPlan(Guid id, Guid payerId, Payer payer, IList<SettlementPlanComponent> settlementPlanComponents)
		{
			if (!payerId.Equals(payer.Id))
			{
				throw new InvalidOperationException();
			}

			Id = id;
			PayerId = payerId;
			Payer = payer;
			PayerId = payer.Id;
			SettlementPlanComponents = settlementPlanComponents;
		}

		private List<SettlementPlanComponent> CreateSettlementPlanComponents(IList<SettlementComponent> settlementComponents)
		{
			var result = new List<SettlementPlanComponent>();

			var currentYear = DateTime.Now.Year;
			for (int month = 1; month <= 12; month++)
			{
				var start = new DateTime(currentYear, month, 1);
				var lastDayInMonth = DateTime.DaysInMonth(currentYear, month);
				var end = new DateTime(currentYear, month, lastDayInMonth);

				result.AddRange(CreateSettlementPlanComponentsForMonth(settlementComponents,
					start,
					end));
			}

			return result;
		}

		private List<SettlementPlanComponent> CreateSettlementPlanComponentsForMonth(IList<SettlementComponent> settlementComponents, DateTime start, DateTime end)
		{
			List<SettlementPlanComponent> result = new List<SettlementPlanComponent>();
			foreach (var settlementComponent in settlementComponents)
			{
				var planComponent = new SettlementPlanComponent(start, end, settlementComponent);
				result.Add(planComponent);
			}

			return result;
		}
	}
}

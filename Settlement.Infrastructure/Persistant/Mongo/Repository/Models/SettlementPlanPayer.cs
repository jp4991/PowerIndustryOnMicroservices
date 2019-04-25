using Settlement.Domain.AggregateModels.PayerAggregate;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;
using System;
using System.Collections.Generic;

namespace Settlement.Infrastructure.Persistant.Mongo.Repository.Models
{
	public class SettlementPlanPayer
	{
		public IList<SettlementPlanComponent> SettlementPlanComponents { get; set; }
		public Guid Id { get; set; }
		public Guid PayerId { get; set; }
		public IList<Payer> Payers { get; set; }
	}
}

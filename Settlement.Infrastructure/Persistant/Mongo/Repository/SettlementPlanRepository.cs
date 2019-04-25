using System;
using System.Linq;
using MongoDB.Driver;
using Settlement.Domain.AggregateModels.PayerAggregate;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;
using Settlement.Infrastructure.Persistant.Mongo.Repository.Models;

namespace Settlement.Infrastructure.Persistant.Mongo.Repository
{
	public class SettlementPlanRepository : ISettlementPlanRepository
	{
		private readonly MongoContext _context;
		private readonly ISettlementPlanFactory _settlementPlanFactory;

		public SettlementPlanRepository(MongoContext context,
			ISettlementPlanFactory settlementPlanFactory)
		{
			_context = context;
			_settlementPlanFactory = settlementPlanFactory;
		}

		public SettlementPlan Add(SettlementPlan settlementPlan)
		{
			_context.SettlementPlans.InsertOne(settlementPlan);
			return settlementPlan;
		}

		public SettlementPlan Get(Guid id)
		{
			var idMatch = id.ToString();

			var settlementPlanPayer = _context.SettlementPlans.Aggregate()
			.Match(x => x.Id.Equals(idMatch))
			.Lookup<SettlementPlan, Payer, SettlementPlanPayer>(_context.Payers,
				l => l.PayerId,
				f => f.Id,
				o => o.Payers
			)
			.Project(c => new
			{
				Id = c.Id,
				PayerId = c.PayerId,
				SettlementPlanComponents = c.SettlementPlanComponents,
				Payer = c.Payers.First()
			})
			.Single();

			var settlementComponentIds = settlementPlanPayer.SettlementPlanComponents
				.Select(x => x.SettlementComponentId)
				.Distinct()
				.ToList();


			var filter = Builders<SettlementComponent>.Filter.In(x => x.Id, settlementComponentIds);

			var settlementComponents = _context.SettlementComponents
				.Find(filter)
				.ToList();

			return _settlementPlanFactory.CreateSettlementPlan(settlementPlanPayer.Id,
				settlementPlanPayer.SettlementPlanComponents,
				settlementPlanPayer.PayerId,
				settlementPlanPayer.Payer,
				settlementComponents);
		}
	}
}

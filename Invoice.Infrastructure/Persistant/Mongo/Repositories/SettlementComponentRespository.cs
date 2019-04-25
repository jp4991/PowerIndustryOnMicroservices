using Invoice.Domain.AggregateModels.SettlementComponentAggreagate;
using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Invoice.Infrastructure.Persistant.Mongo.Repositories
{
	public class SettlementComponentRespository : ISettlementComponentRespository
	{
		readonly MongoContext _mongoContext;

		public SettlementComponentRespository(MongoContext mongoContext)
		{
			_mongoContext = mongoContext;
		}

		public List<SettlementComponentModel> GetSettlementComponentModelList(DateTime startPeriod, DateTime endPeriod, Guid payerId)
		{
			FilterDefinition<SettlementPlan> filterPayerId = Builders<SettlementPlan>.Filter.Eq(x => x.PayerId, payerId);

			var settlelemntPlanComponents = _mongoContext.SettlementPlans.Aggregate()
				.Match(filterPayerId)
				.Project(x => new
				{
					SettlementPlanComponents = x.SettlementPlanComponents.Where(y => y.Start >= startPeriod && y.End <= endPeriod)
				})
				.ToList()
				.SelectMany(x => x.SettlementPlanComponents)
				.ToList();

			var settlementComponentId = settlelemntPlanComponents.Select(x => x.SettlementComponentId)
				.Distinct()
				.ToList();

			var filterSettlementComponentIds = Builders<SettlementComponent>.Filter.In(x => x.Id, settlementComponentId);
			var settlementComponents = _mongoContext.SettlementComponents.Find(filterSettlementComponentIds)
				.ToList();

			List<SettlementComponentModel> settlementComponentModelList = new List<SettlementComponentModel>();
			foreach (var planComponent in settlelemntPlanComponents)
			{
				var settlementComponent = settlementComponents.Single(x => x.Id == planComponent.SettlementComponentId);
				var model = new SettlementComponentModel()
				{
					StartPeriod = planComponent.Start,
					EndPeriod = planComponent.End,
					Price = settlementComponent.UnitPrice,
					SettlementComponentId = settlementComponent.Id
				};
			
				settlementComponentModelList.Add(model);
			}

			return settlementComponentModelList;
		}
	}
}

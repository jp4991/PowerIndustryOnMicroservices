using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;

namespace Settlement.Infrastructure.Persistant.Mongo.Repository
{
	public class SettlementComponentRepository : ISettlementComponentRepository
	{
		private readonly MongoContext _context;

		public SettlementComponentRepository(MongoContext context)
		{
			_context = context;
		}

		public SettlementComponent Add(SettlementComponent settlementComponent)
		{
			_context.SettlementComponents.InsertOne(settlementComponent);
			return settlementComponent;
		}

		public List<SettlementComponent> GetAll()
		{
			return _context.SettlementComponents.FindSync(FilterDefinition<SettlementComponent>.Empty)
				.ToList();
		}
	}
}

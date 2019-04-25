using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Settlement.Domain.AggregateModels.PayerAggregate;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;

namespace Settlement.Infrastructure.Persistant.Mongo
{
	public class MongoContext
	{
		private readonly IMongoDatabase _database;
		private readonly MongoClient mongoClient;

		public MongoContext(IMongoDatabase database, MongoClient mongoClient)
		{
			_database = database;
			this.mongoClient = mongoClient;
		}

		public IMongoCollection<Payer> Payers
		{
			get
			{
				return _database.GetCollection<Payer>("Payers");
			}
		}

		public IMongoCollection<SettlementComponent> SettlementComponents
		{
			get
			{
				return _database.GetCollection<SettlementComponent>("SettlementComponents");
			}
		}

		public IMongoCollection<SettlementPlan> SettlementPlans
		{
			get
			{
				return _database.GetCollection<SettlementPlan>("SettlementPlans");
			}
		}

		public IMongoDatabase Db
		{
			get
			{
				return _database;
			}
		}
	}
}

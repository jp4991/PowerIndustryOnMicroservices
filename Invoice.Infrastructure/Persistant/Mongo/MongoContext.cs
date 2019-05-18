using Invoice.Domain.AggregateModels.PayerAggregate;
using Invoice.Domain.AggregateModels.SettlementComponentAggreagate;
using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using MongoDB.Driver;

namespace Invoice.Infrastructure.Persistant.Mongo
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

		public IMongoCollection<Domain.AggregateModels.Invoice> Invoices
		{
			get
			{
				return _database.GetCollection<Domain.AggregateModels.Invoice>("Invoices");
			}
		}

		public IMongoDatabase Db
		{
			get
			{
				return _database;
			}
		}
		public IMongoCollection<Payer> Payers
		{
			get
			{
				return _database.GetCollection<Payer>("Payers");
			}
		}
	}
}

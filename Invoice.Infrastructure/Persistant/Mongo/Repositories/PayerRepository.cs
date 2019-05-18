using Invoice.Domain.AggregateModels.PayerAggregate;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Infrastructure.Persistant.Mongo.Repositories
{
	public class PayerRepository : IPayerRepository
	{
		private readonly MongoContext _context;

		public PayerRepository(MongoContext context)
		{
			_context = context;
		}

		public Payer Get(Guid id)
		{
			return _context.Payers.FindSync(x => x.Id == id).Single();
		}

		public void Update(Guid id, decimal priceDiscount)
		{
			var update = Builders<Payer>.Update.Set("PriceDiscount", priceDiscount);
			_context.Payers.UpdateOne(x => x.Id == id, update);
		}
	}
}

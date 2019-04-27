using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Settlement.Domain.AggregateModels.PayerAggregate;
using System;
using System.Collections.Generic;

namespace Settlement.Infrastructure.Persistant.Mongo.Repository
{
	public class PayerRepository : IPayerRepository
	{
		private readonly MongoContext _context;

		public PayerRepository(MongoContext context)
		{
			_context = context;
		}

		public Payer Add(Payer payer)
		{
			try
			{
				_context.Payers.InsertOne(payer);
				return payer;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public Payer Get(Guid id)
		{
			return _context.Payers.FindSync(x => x.Id == id).Single();
		}

		public List<Payer> GetAll() => _context.Payers.AsQueryable().ToList();

		public Payer Update(Payer payer)
		{
			throw new NotImplementedException();
		}
	}
}

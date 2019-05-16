using Invoice.Domain.AggregateModels.InvoiceAggregate;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Invoice.Infrastructure.Persistant.Mongo.Repositories
{
	public class InvoiceRepository : IInvoiceRepository
	{
		private readonly MongoContext _context;

		public InvoiceRepository(MongoContext context)
		{
			_context = context;
		}

		public Domain.AggregateModels.Invoice Add(Domain.AggregateModels.Invoice invoice)
		{
			_context.Invoices.InsertOne(invoice);
			return invoice;
		}

		public List<Domain.AggregateModels.Invoice> GetAll(Guid payerId)
		{
			var payerIdForFilter = payerId.ToString();
			var filter = Builders<Domain.AggregateModels.Invoice>.Filter.Eq(x => x.PayerId, payerId);
			return _context.Invoices.FindSync(filter).ToList();
		}
	}
}

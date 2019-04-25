using Invoice.Domain.AggregateModels.InvoiceAggregate;

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
	}
}

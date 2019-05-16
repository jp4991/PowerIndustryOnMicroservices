using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain.AggregateModels.InvoiceAggregate
{
	public interface IInvoiceRepository
	{
		Invoice Add(Invoice invoice);
		List<Invoice> GetAll(Guid payerId);
	}
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain.Services
{
	public interface IInvoiceDomainService
	{
		List<AggregateModels.Invoice> CreateInvoices(DateTime startPeriod, DateTime endPeriod, Guid payerId);
	}
}

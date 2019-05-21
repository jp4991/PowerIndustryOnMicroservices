using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Invoice.Domain.Services.Strategy
{
	public interface IInvoiceServiceStrategy
	{
		Task<List<AggregateModels.Invoice>> CreateInvoices(DateTime startPeriod, DateTime endPeriod, Guid payerId);
	}
}

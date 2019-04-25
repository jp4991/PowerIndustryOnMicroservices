using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain.Services
{
	public interface IInvoiceDomainService
	{
		AggregateModels.Invoice CreateInvoice(DateTime startPeriod, DateTime endPeriod, Guid payerId);
	}
}

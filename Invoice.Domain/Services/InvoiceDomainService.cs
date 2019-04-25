using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain.Services
{
	public class InvoiceDomainService : IInvoiceDomainService
	{
		private readonly ISettlementComponentRespository _settlementComponentRespository;

		public InvoiceDomainService(ISettlementComponentRespository settlementComponentRespository)
		{
			_settlementComponentRespository = settlementComponentRespository;
		}

		public AggregateModels.Invoice CreateInvoice(DateTime startPeriod, DateTime endPeriod, Guid payerId)
		{
			var models = _settlementComponentRespository.GetSettlementComponentModelList(startPeriod,
				endPeriod,
				payerId);

			var invoice = new Domain.AggregateModels.Invoice(payerId);
			foreach (var model in models)
			{
				invoice.AddComponent(model.SettlementComponentId,
					model.StartPeriod,
					model.EndPeriod,
					100,
					model.Price);
			}

			invoice.CalculateValues();

			return invoice;
		}
	}
}

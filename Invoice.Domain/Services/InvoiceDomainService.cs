using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Domain.Services
{
	public class InvoiceDomainService : IInvoiceDomainService
	{
		private readonly ISettlementComponentRespository _settlementComponentRespository;

		public InvoiceDomainService(ISettlementComponentRespository settlementComponentRespository)
		{
			_settlementComponentRespository = settlementComponentRespository;
		}

		public List<AggregateModels.Invoice> CreateInvoices(DateTime startPeriod, DateTime endPeriod, Guid payerId)
		{
			var invoices = new ConcurrentBag<AggregateModels.Invoice>();
			var models = _settlementComponentRespository.GetSettlementComponentModelList(startPeriod,
				endPeriod,
				payerId);

			var groupedByPeriod = models.GroupBy(x => new { x.StartPeriod, x.EndPeriod });
			var parallelResult = Parallel.ForEach(groupedByPeriod,
				new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount },
			(x) =>
			{
				var invoice = new AggregateModels.Invoice(payerId);
				foreach (var model in x)
				{
					invoice.AddComponent(model.SettlementComponentId,
						model.StartPeriod,
						model.EndPeriod,
						100,
						model.Price);
				}

				invoice.CalculateValues();
				invoices.Add(invoice);
			});

			while (!parallelResult.IsCompleted) { }

			return invoices.ToList();
		}
	}
}

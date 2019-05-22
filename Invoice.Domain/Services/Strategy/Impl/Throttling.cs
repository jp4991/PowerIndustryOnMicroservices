using Invoice.Domain.AggregateModels.PayerAggregate;
using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Domain.Services.Strategy.Impl
{
	public class Throttling : IInvoiceServiceStrategy
	{
		public CreateIvoicesStrategyEnum CreateIvoicesStrategyEnum => CreateIvoicesStrategyEnum.Throttling;

		private readonly ISettlementComponentRespository _settlementComponentRespository;
		private readonly IPayerRepository _payerRepository;
		private readonly int CONCURRENCY_LEVEL = 2;
		private Object objectSync = new Object();
		private decimal priceDiscount;

		public Throttling(ISettlementComponentRespository settlementComponentRespository, IPayerRepository payerRepository)
		{
			_settlementComponentRespository = settlementComponentRespository;
			_payerRepository = payerRepository;
		}

		public async Task<List<AggregateModels.Invoice>> CreateInvoices(DateTime startPeriod, DateTime endPeriod, Guid payerId)
		{
			var payer = _payerRepository.Get(payerId);
			priceDiscount = payer.PriceDiscount;

			var models = _settlementComponentRespository.GetSettlementComponentModelList(startPeriod,
				endPeriod,
				payerId);

			var groupedByPeriod = models.GroupBy(x => new { x.StartPeriod, x.EndPeriod }).ToList();

			int nextIndex = 0;
			List<Task<AggregateModels.Invoice>> invoiceTasks = new List<Task<AggregateModels.Invoice>>();
			List<AggregateModels.Invoice> invoices = new List<AggregateModels.Invoice>();

			while (invoiceTasks.Count < CONCURRENCY_LEVEL && nextIndex < groupedByPeriod.Count() - 1)
			{
				var settlementComponentModels = groupedByPeriod[nextIndex].ToList();
				invoiceTasks.Add(Task.Run(() => CreateInvoice(payerId, settlementComponentModels)));
				nextIndex++;
			}

			while(invoiceTasks.Count > 0)
			{
				Task<AggregateModels.Invoice> task = await Task.WhenAny(invoiceTasks);
				invoiceTasks.Remove(task);
				while (invoiceTasks.Count < CONCURRENCY_LEVEL && nextIndex < groupedByPeriod.Count() - 1)
				{
					var settlementComponentModels = groupedByPeriod[nextIndex].ToList();
					invoiceTasks.Add(Task.Run(() => CreateInvoice(payerId, settlementComponentModels)));
					nextIndex++;
				}
				var invoice = await task;
				invoices.Add(invoice);
			}

			return invoices;
		}

		private AggregateModels.Invoice CreateInvoice(Guid payerId, List<SettlementComponentModel> models)
		{
			var invoice = new AggregateModels.Invoice(payerId);
			foreach (var model in models)
			{
				invoice.AddComponent(model.SettlementComponentId,
					model.StartPeriod,
					model.EndPeriod,
					100,
					model.Price);
			}

			lock (objectSync)
			{
				priceDiscount = invoice.CalculateValuesWithDicount(priceDiscount);
			}
			return invoice;
		}
	}
}

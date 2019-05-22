using Invoice.Domain.AggregateModels.PayerAggregate;
using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Domain.Services.Strategy.Impl
{
	public class ParallelLoop : IInvoiceServiceStrategy
	{
		public CreateIvoicesStrategyEnum CreateIvoicesStrategyEnum => CreateIvoicesStrategyEnum.ParallelLoop;

		private readonly ISettlementComponentRespository _settlementComponentRespository;
		private readonly IPayerRepository _payerRepository;

		public ParallelLoop(ISettlementComponentRespository settlementComponentRespository, IPayerRepository payerRepository)
		{
			_settlementComponentRespository = settlementComponentRespository;
			_payerRepository = payerRepository;
		}

		public Task<List<AggregateModels.Invoice>> CreateInvoices(DateTime startPeriod, DateTime endPeriod, Guid payerId)
		{
			var payer = _payerRepository.Get(payerId);
			var priceDiscount = payer.PriceDiscount;
			var objectSync = new Object();

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

				lock (objectSync)
				{
					priceDiscount = invoice.CalculateValuesWithDicount(priceDiscount);
				}
				invoices.Add(invoice);
			});

			while (!parallelResult.IsCompleted) { }
			_payerRepository.Update(payerId, priceDiscount);
			return Task.FromResult(invoices.ToList());
		}
	}
}

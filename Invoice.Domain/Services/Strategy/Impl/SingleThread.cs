using Invoice.Domain.AggregateModels.PayerAggregate;
using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Invoice.Domain.Services.Strategy.Impl
{
	public class SingleThread : IInvoiceServiceStrategy
	{
		public CreateIvoicesStrategyEnum CreateIvoicesStrategyEnum => CreateIvoicesStrategyEnum.SingleThread;

		private readonly ISettlementComponentRespository _settlementComponentRespository;
		private readonly IPayerRepository _payerRepository;

		public SingleThread(ISettlementComponentRespository settlementComponentRespository, IPayerRepository payerRepository)
		{
			_settlementComponentRespository = settlementComponentRespository;
			_payerRepository = payerRepository;
		}

		public Task<List<AggregateModels.Invoice>> CreateInvoices(DateTime startPeriod, DateTime endPeriod, Guid payerId)
		{
			var payer = _payerRepository.Get(payerId);
			var priceDiscount = payer.PriceDiscount;

			var models = _settlementComponentRespository.GetSettlementComponentModelList(startPeriod,
				endPeriod,
				payerId);

			var groupedByPeriod = models.GroupBy(x => new { x.StartPeriod, x.EndPeriod });
			var invoices = new List<AggregateModels.Invoice>(groupedByPeriod.Count());
			foreach (var group in groupedByPeriod)
			{
				var invoice = new AggregateModels.Invoice(payerId);
				foreach (var model in group)
				{
					invoice.AddComponent(model.SettlementComponentId,
						model.StartPeriod,
						model.EndPeriod,
						100,
						model.Price);
				}

				priceDiscount = invoice.CalculateValuesWithDicount(priceDiscount);
				invoices.Add(invoice);
			}

			_payerRepository.Update(payerId, priceDiscount);
			return Task.FromResult(invoices.ToList());
		}
	}
}

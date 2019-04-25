using Settlement.Domain.AggregateModels.PayerAggregate;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Settlement.Domain.Services
{
	public class CreateSettlementPlanService : ICreateSettlementPlanService
	{
		private readonly IPayerRepository _payerRepository;
		private readonly ISettlementComponentRepository _settlementComponentRepository;
		private readonly ISettlementPlanRepository _settlementPlanRepository;

		public CreateSettlementPlanService(IPayerRepository payerRepository,
			ISettlementComponentRepository settlementComponentRepository,
			ISettlementPlanRepository settlementPlanRepositor)
		{
			_payerRepository = payerRepository;
			_settlementComponentRepository = settlementComponentRepository;
			_settlementPlanRepository = settlementPlanRepositor;
		}

		public Task<Guid> CreateSettlementPlant(Guid payerId)
		{
			var payer = _payerRepository.Get(payerId);
			var settlementComponents = _settlementComponentRepository.GetAll();

			var settlementPlan = new SettlementPlan(payer, settlementComponents);
			settlementPlan = _settlementPlanRepository.Add(settlementPlan);

			return Task.FromResult(settlementPlan.Id);
		}
	}
}

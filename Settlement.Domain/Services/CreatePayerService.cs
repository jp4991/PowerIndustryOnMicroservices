using Settlement.Domain.AggregateModels.PayerAggregate;
using System;

namespace Settlement.Domain.Services
{
	public class CreatePayerService : ICreatePayerService
	{
		private readonly IPayerRepository _payerRepository;

		public CreatePayerService(IPayerRepository payerRepository)
		{
			_payerRepository = payerRepository;
		}

		public Guid CreatePayer(string name, decimal priceDiscount)
		{
			var payer = new Payer(name);
			payer = _payerRepository.Add(payer);

			return payer.Id;
		}
	}
}

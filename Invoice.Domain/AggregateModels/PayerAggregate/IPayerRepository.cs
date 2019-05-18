using System;

namespace Invoice.Domain.AggregateModels.PayerAggregate
{
	public interface IPayerRepository
	{
		void Update(Guid id, decimal priceDiscount);
		Payer Get(Guid id);
	}
}

using System;

namespace Settlement.Domain.AggregateModels.PayerAggregate
{
	public interface IPayerRepository
	{
		Payer Add(Payer payer);
		Payer Update(Payer payer);
		Payer Get(Guid id);
	}
}

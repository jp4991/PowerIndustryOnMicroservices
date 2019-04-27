using System;
using System.Collections.Generic;

namespace Settlement.Domain.AggregateModels.PayerAggregate
{
	public interface IPayerRepository
	{
		Payer Add(Payer payer);
		Payer Update(Payer payer);
		Payer Get(Guid id);
		List<Payer> GetAll();
	}
}

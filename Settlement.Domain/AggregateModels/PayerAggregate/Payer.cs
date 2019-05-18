using Settlement.Domain.SeedWork;
using System;

namespace Settlement.Domain.AggregateModels.PayerAggregate
{
	public class Payer : IAggregateRoot
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public decimal PriceDiscount { get; private set; }

		public Payer(string name, decimal priceDiscount)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new InvalidOperationException();
			}

			if (priceDiscount < 0)
			{
				throw new InvalidOperationException();
			}

			Name = name;
			PriceDiscount = priceDiscount;
		}
	}
}

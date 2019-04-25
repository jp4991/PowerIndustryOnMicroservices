using Settlement.Domain.SeedWork;
using System;

namespace Settlement.Domain.AggregateModels.PayerAggregate
{
	public class Payer : IAggregateRoot
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }

		public Payer(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new InvalidOperationException();
			}

			Name = name;
		}
	}
}

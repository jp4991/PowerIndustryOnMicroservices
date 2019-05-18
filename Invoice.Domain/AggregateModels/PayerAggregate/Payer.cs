using Invoice.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain.AggregateModels.PayerAggregate
{
	public class Payer : IAggregateRoot
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public decimal PriceDiscount { get; private set; }
	}
}

using Invoice.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Invoice.Domain.AggregateModels
{
	public class Invoice : IAggregateRoot
	{
		public Guid Id { get; set; }
		public Guid PayerId { get; set; }
		public decimal GrossValue { get; private set; }
		public decimal NetValue { get; private set; }
		public InvoiceStatus Status { get; private set; }
		public IList<InvoiceComponent> Components { get; set; }

		public Invoice()
		{
		}

		public Invoice(Guid payerId)
		{
			PayerId = payerId;
			Components = new List<InvoiceComponent>();
		}

		public void AddComponent(Guid settlementComponentId,
			DateTime startPeriod,
			DateTime endPeriod,
			decimal quantity,
			decimal unitPrice)
		{
			if (Status != InvoiceStatus.New)
			{
				throw new InvalidOperationException();
			}

			var component = new InvoiceComponent(settlementComponentId,
				quantity,
				unitPrice,
				startPeriod,
				endPeriod);

			Components.Add(component);
		}

		public void CalculateValues()
		{
			GrossValue = Components.Sum(x => x.GrossValue);
			NetValue = Components.Sum(x => x.NetValue);
		}

		public decimal CalculateValuesWithDicount(decimal priceDiscount)
		{
			GrossValue = Components.Sum(x => x.GrossValue);
			if (GrossValue < priceDiscount)
			{
				priceDiscount -= GrossValue;
				GrossValue = 0M; ;
			}
			else
			{
				GrossValue -= priceDiscount;
				priceDiscount = 0M;
			}

			NetValue = Components.Sum(x => x.NetValue);
			return priceDiscount;
		}

		public void Calculate()
		{
			if (Status != InvoiceStatus.New)
			{
				throw new InvalidOperationException();
			}

			GrossValue = Components.Sum(x => x.GrossValue);
			NetValue = Components.Sum(x => x.NetValue);
		}

		public void Approve()
		{
			if (Components == null 
				|| !Components.Any())
			{
				throw new InvalidOperationException();
			}

			Status = InvoiceStatus.Approved;
		}
	}
}

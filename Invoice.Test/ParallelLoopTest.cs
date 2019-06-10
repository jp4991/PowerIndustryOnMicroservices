using Invoice.Domain.AggregateModels.PayerAggregate;
using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using Invoice.Domain.Services.Strategy.Impl;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;

namespace Invoice.Test
{
	public class ParallelLoopTest
	{
		[Fact]
		public void ShouldCreate36Invoices()
		{
			var settlementComponentRespositoryMock = new Mock<ISettlementComponentRespository>();
			List<SettlementComponentModel> models = SettlementComponentModelFactory.CreateModels(new List<int> { 2017, 2018, 2019 });
			settlementComponentRespositoryMock.Setup(s => s.GetSettlementComponentModelList(It.IsAny<DateTime>(),
				It.IsAny<DateTime>(),
				It.IsAny<Guid>()))
				.Returns(() => models);


			var payerRepository = new Mock<IPayerRepository>();
			payerRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns(() => new Payer());
			var samples = new List<double>();

			var service = new ParallelLoop(settlementComponentRespositoryMock.Object,
				payerRepository.Object);
			var start = new DateTime(2017, 1, 1);
			var end = new DateTime(2019, 12, 31);
			var st = new Stopwatch();

			for (int i = 0; i < 10; i++)
			{
				st.Start();
				var invoices = service.CreateInvoices(start, end, new Guid()).Result;
				st.Stop();

				Assert.Equal(36, invoices.Count);
				samples.Add(st.Elapsed.TotalMilliseconds);
				st.Reset();
			}

			var result = samples.Sum(s => s) / (double)samples.Count();
		}
	}
}

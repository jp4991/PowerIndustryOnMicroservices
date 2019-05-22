using Invoice.Domain.AggregateModels.PayerAggregate;
using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using Invoice.Domain.Services.Strategy.Impl;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace Invoice.Test
{
	public class SingleThreadTest
	{
		[Fact]
		public void Test()
		{
			var settlementComponentRespositoryMock = new Mock<ISettlementComponentRespository>();
			List<SettlementComponentModel> models = SettlementComponentModelFactory.CreateModels();
			settlementComponentRespositoryMock.Setup(s => s.GetSettlementComponentModelList(It.IsAny<DateTime>(),
				It.IsAny<DateTime>(),
				It.IsAny<Guid>()))
				.Returns(() => models);


			var payerRepository = new Mock<IPayerRepository>();
			payerRepository.Setup(s => s.Get(It.IsAny<Guid>())).Returns(() => new Payer());

			var service = new SingleThread(settlementComponentRespositoryMock.Object,
				payerRepository.Object);
			var start = new DateTime(2019, 1, 1);
			var end = new DateTime(2019, 12, 31);
			var st = new Stopwatch();

			st.Start();
			var invoices = service.CreateInvoices(start, end, new Guid()).Result;
			st.Stop();

			Assert.Equal(12, invoices.Count);
			var elapsed = st.Elapsed;
		}
	}
}

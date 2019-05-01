using System.Threading;
using System.Threading.Tasks;
using Invoice.Domain.AggregateModels.InvoiceAggregate;
using Invoice.Domain.Services;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;

namespace Invoice.API.Application.Command.QueueHandlers
{
	public class CreateInvoiceCommandHandler : ICommandHandler<CreateInvoiceCommand>
	{
		private readonly IInvoiceRepository _invoiceRepository;
		private readonly IInvoiceDomainService _invoiceDomainService;

		public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository, 
			IInvoiceDomainService invoiceDomainService)
		{
			_invoiceRepository = invoiceRepository;
			_invoiceDomainService = invoiceDomainService;
		}

		public Task HandleAsync(CreateInvoiceCommand command, CancellationToken cancellationToken)
		{
			var invoice = _invoiceDomainService.CreateInvoice(command.StartPeriod, command.EndPeriod, command.PayerId);
			_invoiceRepository.Add(invoice);

			return Task.FromResult(invoice);
		}
	}
}

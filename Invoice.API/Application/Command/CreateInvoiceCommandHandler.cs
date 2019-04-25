using Invoice.Domain.AggregateModels.InvoiceAggregate;
using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using Invoice.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Invoice.API.Application.Command
{
	//Application services
	public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, CreateInvoiceResponse>
	{
		private readonly IInvoiceRepository _invoiceRepository;
		private readonly IInvoiceDomainService _invoiceDomainService;

		public CreateInvoiceCommandHandler(IInvoiceRepository invoiceRepository,
			IInvoiceDomainService invoiceDomainService)
		{
			_invoiceRepository = invoiceRepository;
			_invoiceDomainService = invoiceDomainService;
		}

		public Task<CreateInvoiceResponse> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var invoice = _invoiceDomainService.CreateInvoice(request.StartPeriod, request.EndPeriod, request.PayerId);
				invoice = _invoiceRepository.Add(invoice);
				var respons = new CreateInvoiceResponse()
				{
					Success = true,
					InvoiceId = invoice.Id
				};

				return Task.FromResult(respons);
			}
			catch
			{
				var respons = new CreateInvoiceResponse()
				{
					Success = false,
					ErrorDescription = "Wystąpił błąd w trkacie tworzenia faktury."
				};

				return Task.FromResult(respons);
			}
		}
	}
}

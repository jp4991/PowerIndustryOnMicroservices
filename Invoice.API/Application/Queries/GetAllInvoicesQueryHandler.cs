using Invoice.API.Dto;
using Invoice.Domain.AggregateModels.InvoiceAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Invoice.API.Application.Queries
{
	public class GetAllInvoicesQueryHandler : IRequestHandler<GetAllInvoicesQuery, List<InvoiceDto>>
	{

		private IInvoiceRepository _invoiceRepository;

		public GetAllInvoicesQueryHandler(IInvoiceRepository invoiceRepository)
		{
			_invoiceRepository = invoiceRepository;
		}

		public Task<List<InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
		{
			List<InvoiceDto> invoiceDtos = new List<InvoiceDto>();
			var invoices = _invoiceRepository.GetAll(request.PayerId);
			foreach(var invoice in invoices)
			{
				var invoiceDto = new InvoiceDto()
				{
					Id = invoice.Id,
					GrossValue = invoice.GrossValue,
					NetValue = invoice.NetValue,
					PayerId = invoice.PayerId,
					Status = invoice.Status,
					Components = new List<InvoiceComponentDto>()
				};

				foreach(var component in invoice.Components)
				{
					var componentDto = new InvoiceComponentDto()
					{
						SettlementComponentId = component.SettlementComponentId,
						EndPeriod = component.EndPeriod,
						StartPeriod = component.StartPeriod,
						GrossValue = component.GrossValue,
						NetValue = component.NetValue,
						Quantity = component.Quantity
					};
					invoiceDto.Components.Add(componentDto);
				}

				invoiceDtos.Add(invoiceDto);
			}

			return Task.FromResult(invoiceDtos);
		}
	}
}

using Invoice.API.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace Invoice.API.Application.Queries
{
	public class GetAllInvoicesQuery : IRequest<List<InvoiceDto>>
	{
		public Guid PayerId { get; set; }
	}
}

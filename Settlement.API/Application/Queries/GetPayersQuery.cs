using MediatR;
using Settlement.API.Dto;
using System.Collections.Generic;

namespace Settlement.API.Application.Queries
{
	public class GetPayersQuery : IRequest<List<PayerDto>>
	{
	}
}

using MediatR;
using Settlement.API.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Settlement.API.Application.Queries
{
	public class GetPayersQuery : IRequest<List<PayerDto>>
	{
	}
}

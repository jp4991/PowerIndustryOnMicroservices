using MediatR;
using Settlement.API.Dto;
using System;

namespace Settlement.API.Application.Queries
{
	public class GetSettlementPlanQuery : IRequest<SettlementPlanDto>
	{
		public Guid SettlementPlanId { get; set; }
	}
}

using MediatR;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Settlement.API.Application.Queries
{
	public class GetSettlementPlanQuery : IRequest<SettlementPlan>
	{
		public Guid SettlementPlanId { get; set; }
	}
}

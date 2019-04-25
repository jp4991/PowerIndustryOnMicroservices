using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Settlement.Domain.Services
{
	public interface ICreateSettlementPlanService
	{
		Task<Guid> CreateSettlementPlant(Guid payerId);
	}
}

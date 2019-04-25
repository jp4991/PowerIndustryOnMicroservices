using System;

namespace Settlement.Domain.Services
{
	public interface ICreateSettlementComponentService
	{
		Guid CreateSettlementComponent(string name, decimal unitPrice);
	}
}

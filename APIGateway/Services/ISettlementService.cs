using APIGateway.Dto;
using APIGateway.Dto.Settlement;
using RestEase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIGateway.Services
{
	public interface ISettlementService
	{
		[Get("/api/values")]
		Task<IEnumerable<string>> GetValuesAsync();

		[Get("/api/SettlementComponent/GetAllSettlementComponents")]
		Task<List<SettlementComponentDto>> GetAllSettlementComponentsAsync();

		[Get("/api/SettlementPlan/GetSettlementPlan")]
		Task<SettlementPlanDto> GetSettlementPlan(Guid id);

		[Get("/api/Payer/GetAllPayers")]
		Task<List<PayerDto>> GetAllPayers();
	}
}

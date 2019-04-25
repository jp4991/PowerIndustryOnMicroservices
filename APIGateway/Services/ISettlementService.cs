using APIGateway.Dto;
using RestEase;
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
	}
}

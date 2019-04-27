using MediatR;
using Settlement.API.Dto;
using Settlement.Domain.AggregateModels.PayerAggregate;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Settlement.API.Application.Queries
{
	public class GetPayersQueryHandler : IRequestHandler<GetPayersQuery, List<PayerDto>>
	{
		private readonly IPayerRepository _payerRepository;

		public GetPayersQueryHandler(IPayerRepository payerRepository)
		{
			_payerRepository = payerRepository;
		}

		public Task<List<PayerDto>> Handle(GetPayersQuery request, CancellationToken cancellationToken)
		{
			var domainPayers = _payerRepository.GetAll();

			var payerDtos = domainPayers.Select(x => new PayerDto()
			{
				Id = x.Id,
				Name = x.Name
			})
			.ToList();

			return Task.FromResult(payerDtos);
		}
	}
}

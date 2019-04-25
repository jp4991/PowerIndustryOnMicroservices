using MediatR;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using Settlement.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Settlement.API.Application.Commands
{
	public class CreateSettlementComponentCommandHandler : IRequestHandler<CreateSettlementComponentCommand, CreateSettlementComponentResponse>
	{
		private readonly ICreateSettlementComponentService _createSettlementComponentService;

		public CreateSettlementComponentCommandHandler(ICreateSettlementComponentService createSettlementComponentService)
		{
			_createSettlementComponentService = createSettlementComponentService;
		}

		public Task<CreateSettlementComponentResponse> Handle(CreateSettlementComponentCommand request, CancellationToken cancellationToken)
		{
			var response = new CreateSettlementComponentResponse();
			try
			{
				response.SettlementComponentId = _createSettlementComponentService.CreateSettlementComponent(request.Name, request.UnitPrice);
				response.Success = true;

				return Task.FromResult(response);
			}
			catch (Exception)
			{
				response.Success = false;
				response.ErrorDescription = "Błąd w trakcie tworzenia składnika rolziczenia";
				return Task.FromResult(response);
			}
		}
	}
}

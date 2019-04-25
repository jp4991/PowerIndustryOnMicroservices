using MediatR;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using Settlement.Domain.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Settlement.API.Application.Commands.QueueHandlers
{
	public class CreateSettlementComponentCommandHandler : ICommandHandler<CreateSettlementComponentCommand>
	{
		private readonly ICreateSettlementComponentService _createSettlementComponentService;

		public CreateSettlementComponentCommandHandler(ICreateSettlementComponentService createSettlementComponentService)
		{
			_createSettlementComponentService = createSettlementComponentService;
		}

		public async Task HandleAsync(CreateSettlementComponentCommand command, CancellationToken cancellationToken)
		{
			await Task.FromResult(_createSettlementComponentService.CreateSettlementComponent(command.Name, command.UnitPrice));
		}
	}
}

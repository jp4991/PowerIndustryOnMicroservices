using System.Threading;
using System.Threading.Tasks;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using Settlement.Domain.Services;

namespace Settlement.API.Application.Commands.QueueHandlers
{
	public class CreatePayerCommandHandler : ICommandHandler<CreatePayerCommand>
	{
		private readonly ICreatePayerService _createPayerService;

		public CreatePayerCommandHandler(ICreatePayerService createPayerService)
		{
			_createPayerService = createPayerService;
		}

		public Task HandleAsync(CreatePayerCommand command, CancellationToken cancellationToken)
		{
			return Task.FromResult(_createPayerService.CreatePayer(command.Name));
		}
	}
}

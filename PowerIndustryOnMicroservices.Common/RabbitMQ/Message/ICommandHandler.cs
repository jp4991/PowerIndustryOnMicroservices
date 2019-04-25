using System.Threading;
using System.Threading.Tasks;

namespace PowerIndustryOnMicroservices.Common.RabbitMQ.Message
{
	public interface ICommandHandler<T> where T : ICommand
	{
		Task HandleAsync(T command, CancellationToken cancellationToken);
	}
}

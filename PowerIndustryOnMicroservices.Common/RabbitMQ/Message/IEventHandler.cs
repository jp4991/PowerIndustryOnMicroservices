namespace PowerIndustryOnMicroservices.Common.RabbitMQ.Message
{
	public interface IEventHandler<T> where T : IEvent
	{
	}
}

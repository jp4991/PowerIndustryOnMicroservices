using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Instantiation;

namespace PowerIndustryOnMicroservices.Common.RabbitMQ
{
	public static class ServiceCollectionExtensions
	{
		public static void AddRabbitMq(this IServiceCollection services, IConfigurationSection section)
		{
			var rawRabbitOptions = new RawRabbitOptions
			{
				ClientConfiguration = GetRawRabbitConfiguration()
			};
			var busClient = RawRabbitFactory.CreateSingleton();

			services.AddSingleton<IBusClient, RawRabbit.Instantiation.Disposable.BusClient>(_ => busClient);
		}

		private static RawRabbitConfiguration GetRawRabbitConfiguration() => new RawRabbitConfiguration()
		{
			Username = "guest",
			Password = "guest",
			Port = 5672,
			VirtualHost = "/",
			Hostnames = { "localhost" }
		};
	}
}

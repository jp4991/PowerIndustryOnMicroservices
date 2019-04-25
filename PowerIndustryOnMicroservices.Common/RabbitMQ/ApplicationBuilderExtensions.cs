using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using RawRabbit;
using System;
using System.Threading;

namespace PowerIndustryOnMicroservices.Common.RabbitMQ
{
	public static class ApplicationBuilderExtensions
	{
		public static IApplicationBuilder AddHandlerForCommand<T>(this IApplicationBuilder app,
			string exchangeName,
			string queueName)
			where T : ICommand
		{
			if (!(app.ApplicationServices.GetRequiredService(typeof(ICommandHandler<T>)) is ICommandHandler<T> handler))
			{
				throw new NullReferenceException();
			}

			if (!(app.ApplicationServices.GetService(typeof(IBusClient)) is IBusClient busClient))
			{
				throw new NullReferenceException();
			}

			var qq = busClient.SubscribeAsync<T>(async (msg) =>
				{
					await handler.HandleAsync(msg, CancellationToken.None);
				}, 
				CFG => CFG.UseSubscribeConfiguration(F => F.OnDeclaredExchange(E => E.WithName(exchangeName))
					.FromDeclaredQueue(q => q.WithName(queueName)))
			);

			return app;
		}
	}
}

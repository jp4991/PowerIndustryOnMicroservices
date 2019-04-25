using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Settlement.Infrastructure.Persistant.Mongo;
using Settlement.Domain.AggregateModels.PayerAggregate;
using Settlement.Infrastructure.Persistant.Mongo.Repository;
using MongoDB.Bson.Serialization;
using System;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson;
using Settlement.Infrastructure.Persistant.Mongo.Maps;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;
using MongoDB.Driver;
using PowerIndustryOnMicroservices.Common.RabbitMQ;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using Settlement.API.Application.Commands;
using Settlement.Domain.Services;
using Consul;

namespace Settlement.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddMediatR();

			//Instantination mongodb
			MongoClient client = new MongoClient(Configuration.GetSection("MongoConnection:ConnectionString").Value);
			IMongoDatabase database = client.GetDatabase(Configuration.GetSection("MongoConnection:Database").Value);
			services.AddSingleton(client);
			services.AddSingleton(database);
			services.AddTransient<MongoContext>();

			services.AddTransient<IPayerRepository, PayerRepository>();
			services.AddTransient<ISettlementComponentRepository, SettlementComponentRepository>();
			services.AddTransient<ISettlementPlanFactory, SettlementPlanFactory>();
			services.AddTransient<ISettlementPlanRepository, SettlementPlanRepository>();
			services.AddTransient<ICreateSettlementComponentService, CreateSettlementComponentService>();
			services.AddTransient<ICreatePayerService, CreatePayerService>();
			services.AddTransient<ICreateSettlementPlanService, CreateSettlementPlanService>();

			services.AddTransient<ICommandHandler<CreateSettlementComponentCommand>,
				Application.Commands.QueueHandlers.CreateSettlementComponentCommandHandler>();

			services.AddTransient<ICommandHandler<CreatePayerCommand>,
				Application.Commands.QueueHandlers.CreatePayerCommandHandler>();

			services.AddTransient<ICommandHandler<CreateSettlementPlanCommand>,
				Application.Commands.QueueHandlers.CreateSettlementPlanCommandHandler>();

			services.AddRabbitMq(Configuration.GetSection("rabbitmq"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
		{
			var consulClient = new ConsulClient();

			var serviceId = Guid.NewGuid().ToString();
			var agentReg = new AgentServiceRegistration()
			{
				ID = serviceId,
				Name = "SettlementService",
				Port = 60927,
				Check = new AgentServiceCheck
				{
					Interval = TimeSpan.FromSeconds(5),
					DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
					HTTP = "http://localhost:60927/api/healthcheck/check"
				},
				Tags = new[]
				{
					"urlprefix-/api/values",
					"urlprefix-/api/SettlementComponent/GetAllSettlementComponents"
				}
			};

			consulClient.Agent.ServiceRegister(agentReg).GetAwaiter().GetResult();
			lifetime.ApplicationStopped.Register(() => consulClient.Agent.ServiceDeregister(serviceId));
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseFileServer();
			app.UseHttpsRedirection();
			app.UseMvc();
			MongoDbConfiguration();

			app.AddHandlerForCommand<CreateSettlementComponentCommand>("", "createsettlementcomponent");
			app.AddHandlerForCommand<CreatePayerCommand>("", "createpayer");
			app.AddHandlerForCommand<CreateSettlementPlanCommand>("", "createsettlementplan");
		}

		private void MongoDbConfiguration()
		{
			BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
			BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Local, BsonType.DateTime));
			PayerMap.Map();
			SettlementPlanComponentMap.Map();
			SettlementComponentMap.Map();
			SettlementPlanMap.Map();
		}
	}
}

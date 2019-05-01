using Invoice.API.Application.Command;
using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using Invoice.Infrastructure.Persistant.Mongo;
using Invoice.Infrastructure.Persistant.Mongo.Maps;
using Invoice.Infrastructure.Persistant.Mongo.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PowerIndustryOnMicroservices.Common.RabbitMQ;
using PowerIndustryOnMicroservices.Common.RabbitMQ.Message;
using System;
using System.Collections.Generic;

namespace Invoice.API
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

			services.AddTransient<ISettlementComponentRespository, SettlementComponentRespository>();
			services.AddTransient<Domain.AggregateModels.InvoiceAggregate.IInvoiceRepository, InvoiceRepository>();
			services.AddTransient<Domain.Services.IInvoiceDomainService, Domain.Services.InvoiceDomainService>();

			services.AddTransient<ICommandHandler<CreateInvoiceCommand>,
				Application.Command.QueueHandlers.CreateInvoiceCommandHandler>();

			services.AddRabbitMq(Configuration.GetSection("rabbitmq"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
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

			app.AddHandlerForCommand<CreateInvoiceCommand>("", "createinvoice");
		}

		private void MongoDbConfiguration()
		{
			BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
			BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Local, BsonType.DateTime));
			SettlementPlanComponentMap.Map();
			SettlementComponentMap.Map();
			SettlementPlanMap.Map();
			InvoiceComponent.Map();
			InvoiceMap.Map();
		}
	}
}

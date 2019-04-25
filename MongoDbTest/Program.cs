using MongoDB.Bson;
using MongoDB.Driver;
using Settlement.Infrastructure.Persistant.Mongo;
using System.Linq;
using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Settlement.Infrastructure.Persistant.Mongo.Maps;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;
using System.Collections.Generic;
using Settlement.Domain.AggregateModels.PayerAggregate;
using System.Threading.Tasks;
using RabbitMQ.Client;
using System.Text;

namespace MongoDbTest
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			using (var connection = factory.CreateConnection())
			using (var channel = connection.CreateModel())
			{
				channel.QueueDeclare(queue: "msgKey",
					durable: false,
					exclusive: false,
					autoDelete: false,
					arguments: null);

				var body = Encoding.UTF8.GetBytes("Hello RabbitMq!");
				channel.BasicPublish(exchange: "",
					routingKey: "msgKey",
					basicProperties: null,
					body: body);
				Console.WriteLine($"Send: {body}");
			}
				Console.ReadKey();
		}

		public static async Task CallAsync()
		{
			var r1 = GetAsync(10);
			Console.WriteLine($"Result {r1}");
			var r2 = await GetAsync(20);
			Console.WriteLine($"Result {r2}");
		}

		public static Task<int> GetAsync(int i)
		{
			var task = Task.Run(() => 
			{
				Console.WriteLine($"Async method: {i}");
				return i;
			});
			return task;
		}

		private static void MongoDbSettings()
		{
			BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(BsonType.String));
			BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Local, BsonType.String));
			PayerMap.Map();
			SettlementPlanComponentMap.Map();
			SettlementComponentMap.Map();
		}

		public class SettlementPlanPayer
		{
			public IList<SettlementPlanComponent> SettlementPlanComponents { get; set; }
			public int Id { get; set; }
			public Guid PayerId { get; set; }
			public IList<Payer> Payers { get; set; }
		}

		//public class SettlementPlanPayerMap
		//{
		//	public static void Map()
		//	{
		//		BsonClassMap.RegisterClassMap<SettlementPlanPayer>(map =>
		//		{
		//			map.AutoMap();
		//			map.MapIdMember(x => x.Id);
		//			map.MapMember(x => x.Payers);
		//			map.MapMember(x => x.SettlementPlanComponents);
		//			map.SetIgnoreExtraElements(true);
		//		});
		//	}
		//}
	}
}

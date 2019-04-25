using MongoDB.Bson.Serialization;
using Settlement.Domain.AggregateModels.SettlementComponentAggregate;

namespace Settlement.Infrastructure.Persistant.Mongo.Maps
{
	public class SettlementComponentMap
	{
		public static void Map()
		{
			BsonClassMap.RegisterClassMap<SettlementComponent>(map =>
			{
				map.AutoMap();
				map.MapIdMember(x => x.Id);
				map.MapMember(x => x.Name);
				map.MapMember(x => x.UnitPrice);
			});
		}
	}
}

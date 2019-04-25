using Invoice.Domain.AggregateModels.SettlementComponentAggreagate;
using MongoDB.Bson.Serialization;

namespace Invoice.Infrastructure.Persistant.Mongo.Maps
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

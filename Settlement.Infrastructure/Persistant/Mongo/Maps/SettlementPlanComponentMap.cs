using MongoDB.Bson.Serialization;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;

namespace Settlement.Infrastructure.Persistant.Mongo.Maps
{
	public class SettlementPlanComponentMap
	{
		public static void Map()
		{
			BsonClassMap.RegisterClassMap<SettlementPlanComponent>(map =>
			{
				map.AutoMap();
				map.MapMember(x => x.SettlementComponentId);
				map.MapMember(x => x.Start);
				map.MapMember(x => x.End);
				map.UnmapMember(x => x.SettlementComponent);
				map.SetIgnoreExtraElements(true);
			});
		}
	}
}

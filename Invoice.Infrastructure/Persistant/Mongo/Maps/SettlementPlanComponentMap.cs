using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using MongoDB.Bson.Serialization;

namespace Invoice.Infrastructure.Persistant.Mongo.Maps
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
				map.SetIgnoreExtraElements(true);
			});
		}
	}
}

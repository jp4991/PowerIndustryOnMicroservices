using MongoDB.Bson.Serialization;
using Settlement.Domain.AggregateModels.SettlementPlanAggregate;

namespace Settlement.Infrastructure.Persistant.Mongo.Maps
{
	public class SettlementPlanMap
	{
		public static void Map()
		{
			BsonClassMap.RegisterClassMap<SettlementPlan>(map =>
			{
				map.AutoMap();
				//map.MapIdMember(x => x.Id);
				map.MapMember(x => x.SettlementPlanComponents);
				map.MapMember(x => x.PayerId);
				map.UnmapMember(x => x.Payer);
				map.SetIgnoreExtraElements(true);
			});
		}
	}
}

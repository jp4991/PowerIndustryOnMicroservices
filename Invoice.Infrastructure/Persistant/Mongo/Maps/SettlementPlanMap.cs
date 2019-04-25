using Invoice.Domain.AggregateModels.SettlementPlanAggreagate;
using MongoDB.Bson.Serialization;

namespace Invoice.Infrastructure.Persistant.Mongo.Maps
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
				map.SetIgnoreExtraElements(true);
			});
		}
	}
}

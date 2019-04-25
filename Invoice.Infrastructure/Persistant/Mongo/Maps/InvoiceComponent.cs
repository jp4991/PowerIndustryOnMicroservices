using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Infrastructure.Persistant.Mongo.Maps
{
	public class InvoiceComponent
	{
		public static void Map()
		{
			BsonClassMap.RegisterClassMap<Domain.AggregateModels.InvoiceComponent>(map =>
			{
				map.AutoMap();
				map.MapMember(x => x.EndPeriod);
				map.MapMember(x => x.StartPeriod);
				map.MapMember(x => x.GrossValue);
				map.MapMember(x => x.NetValue);
				map.MapMember(x => x.SettlementComponentId);
			});
		}
	}
}

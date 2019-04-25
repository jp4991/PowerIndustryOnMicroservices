using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Infrastructure.Persistant.Mongo.Maps
{
	public class InvoiceMap
	{
		public static void Map()
		{
			BsonClassMap.RegisterClassMap<Domain.AggregateModels.Invoice>(map =>
			{
				map.AutoMap();
				map.MapIdMember(x => x.Id);
				map.MapMember(x => x.Components);
				map.MapMember(x => x.GrossValue);
				map.MapMember(x => x.NetValue);
				map.MapMember(x => x.PayerId);
				map.MapMember(x => x.Status);
			});
		}
	}
}

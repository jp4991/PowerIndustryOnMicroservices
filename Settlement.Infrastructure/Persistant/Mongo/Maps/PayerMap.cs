﻿using MongoDB.Bson.Serialization;
using Settlement.Domain.AggregateModels.PayerAggregate;

namespace Settlement.Infrastructure.Persistant.Mongo.Maps
{
	public class PayerMap
	{
		public static void Map()
		{
			BsonClassMap.RegisterClassMap<Payer>(map =>
			{
				map.AutoMap();
				map.MapIdMember(x => x.Id);
				map.MapMember(x => x.Name).SetIsRequired(true);
			});
		}
	}
}

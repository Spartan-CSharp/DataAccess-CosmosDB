using System;
using System.Collections.Generic;
using System.Linq;

using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccessLibrary.MongoDBDataAccess
{
	internal static class MongoDBDataAccess
	{
		internal static void CreateRecord<T>(this IMongoDatabase mongoDatabase, string table, T record)
		{
			IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(table);
			collection.InsertOne(record);
		}

		internal static T RetrieveRecordById<T>(this IMongoDatabase mongoDatabase, string table, Guid id)
		{
			IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(table);
			BsonBinaryData bindata = new BsonBinaryData(id, GuidRepresentation.Standard);
			BsonDocument filter = new BsonDocument("_id", bindata);

			T output = collection.Find(filter).FirstOrDefault();

			return output;
		}

		internal static List<T> RetrieveRecords<T>(this IMongoDatabase mongoDatabase, string table)
		{
			IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(table);
			BsonDocument filter = new BsonDocument();

			List<T> output = collection.Find(filter).ToList();

			return output;
		}

		internal static void UpdateRecord<T>(this IMongoDatabase mongoDatabase, string table, Guid id, T record)
		{
			IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(table);
			BsonBinaryData bindata = new BsonBinaryData(id, GuidRepresentation.Standard);
			BsonDocument filter = new BsonDocument("_id", bindata);

			_ = collection.ReplaceOne(filter, record);
		}

		internal static void DeleteRecord<T>(this IMongoDatabase mongoDatabase, string table, Guid id)
		{
			IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(table);
			FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", id);
			_ = collection.DeleteOne(filter);
		}
	}
}

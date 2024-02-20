using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

namespace DataAccessLibrary.MongoDBDataAccess
{
	internal static class MongoDBDataAccess
	{
		internal static async Task CreateRecordAsync<T>(this IMongoDatabase mongoDatabase, string table, T record)
		{
			IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(table);
			await collection.InsertOneAsync(record);
		}

		internal static async Task<T> RetrieveRecordByIdAsync<T>(this IMongoDatabase mongoDatabase, string table, Guid id)
		{
			IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(table);
			BsonBinaryData bindata = new BsonBinaryData(id, GuidRepresentation.Standard);
			BsonDocument filter = new BsonDocument("_id", bindata);

			T output = (await collection.FindAsync(filter)).FirstOrDefault();

			return output;
		}

		internal static async Task<List<T>> RetrieveRecordsAsync<T>(this IMongoDatabase mongoDatabase, string table)
		{
			IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(table);
			BsonDocument filter = new BsonDocument();

			List<T> output = (await collection.FindAsync(filter)).ToList();

			return output;
		}

		internal static async Task UpdateRecordAsync<T>(this IMongoDatabase mongoDatabase, string table, Guid id, T record)
		{
			IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(table);
			BsonBinaryData bindata = new BsonBinaryData(id, GuidRepresentation.Standard);
			BsonDocument filter = new BsonDocument("_id", bindata);

			await collection.ReplaceOneAsync(filter, record);
		}

		internal static async Task DeleteRecordAsync<T>(this IMongoDatabase mongoDatabase, string table, Guid id)
		{
			IMongoCollection<T> collection = mongoDatabase.GetCollection<T>(table);
			FilterDefinition<T> filter = Builders<T>.Filter.Eq("Id", id);
			await collection.DeleteOneAsync(filter);
		}
	}
}

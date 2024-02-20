using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.Cosmos;

namespace DataAccessLibrary.CosmosDBDataAccess
{
	internal static class CosmosDBDataAccess
	{
		internal static async Task CreateRecordAsync<T>(this Database cosmosDatabase, string containerName, T record)
		{
			Container container = cosmosDatabase.GetContainer(containerName);
			await container.CreateItemAsync(record);
		}

		internal static async Task<T> RetrieveRecordByIdAsync<T>(this Database cosmosDatabase, string containerName, string id)
		{
			Container container = cosmosDatabase.GetContainer(containerName);
			string sql = "SELECT * FROM c WHERE c.id = @Id";
			QueryDefinition queryDefinition = new QueryDefinition(sql).WithParameter("@Id", id);
			FeedIterator<T> feedIterator = container.GetItemQueryIterator<T>(queryDefinition);
			while ( feedIterator.HasMoreResults )
			{
				FeedResponse<T> currentResultsSet = await feedIterator.ReadNextAsync();
				foreach ( T item in currentResultsSet )
				{
					return item;
				}
			}

			throw new ApplicationException($"Record with ID {id} not found");
		}

		internal static async Task<List<T>> RetrieveRecordsAsync<T>(this Database cosmosDatabase, string containerName)
		{
			Container container = cosmosDatabase.GetContainer(containerName);
			string sql = "SELECT * FROM c";
			QueryDefinition queryDefinition = new QueryDefinition(sql);
			FeedIterator<T> feedIterator = container.GetItemQueryIterator<T>(queryDefinition);
			List<T> output = new List<T>();
			while ( feedIterator.HasMoreResults )
			{
				FeedResponse<T> currentResultsSet = await feedIterator.ReadNextAsync();
				foreach ( T item in currentResultsSet )
				{
					output.Add(item);
				}
			}

			return output;
		}

		internal static async Task UpdateRecordAsync<T>(this Database cosmosDatabase, string containerName, T record)
		{
			Container container = cosmosDatabase.GetContainer(containerName);
			await container.UpsertItemAsync(record);
		}

		internal static async Task DeleteRecordAsync<T>(this Database cosmosDatabase, string containerName, string id, PartitionKey partitionKey)
		{
			Container container = cosmosDatabase.GetContainer(containerName);
			await container.DeleteItemAsync<T>(id, partitionKey);
		}
	}
}

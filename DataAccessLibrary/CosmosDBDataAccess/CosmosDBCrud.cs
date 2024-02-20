using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using DataAccessLibrary.Models;

using Microsoft.Azure.Cosmos;

namespace DataAccessLibrary.CosmosDBDataAccess
{
	public class CosmosDBCrud : ICrud
	{
		private readonly Database _database;

		public CosmosDBCrud(string endpointUri, string primaryKey, string databaseName)
		{
			CosmosClient cosmosClient = new CosmosClient(endpointUri, primaryKey);
			_database = cosmosClient.GetDatabase(databaseName);
		}

		public async Task CreateAddressAsync(AddressModel address)
		{
			await _database.CreateRecordAsync("Addresses", address);
		}

		public async Task CreateEmployerAsync(EmployerModel employer)
		{
			await _database.CreateRecordAsync("Employers", employer);
		}

		public async Task CreatePersonAsync(PersonModel person)
		{
			await _database.CreateRecordAsync("People", person);
		}

		public async Task DeleteAddressAsync(AddressModel address)
		{
			await _database.DeleteRecordAsync<AddressModel>("Addresses", address.Id.ToString(), new PartitionKey(address.ZipCode));
		}

		public async Task DeleteEmployerAsync(EmployerModel employer)
		{
			await _database.DeleteRecordAsync<AddressModel>("Employers", employer.Id.ToString(), new PartitionKey(employer.CompanyName));
		}

		public async Task DeletePersonAsync(PersonModel person)
		{
			await _database.DeleteRecordAsync<AddressModel>("People", person.Id.ToString(), new PartitionKey(person.LastName));
		}

		public async Task<AddressModel> RetrieveAddressByIdAsync(Guid id)
		{
			AddressModel output = await _database.RetrieveRecordByIdAsync<AddressModel>("Addresses", id.ToString());
			return output;
		}

		public async Task<List<AddressModel>> RetrieveAllAddressesAsync()
		{
			List<AddressModel> output = await _database.RetrieveRecordsAsync<AddressModel>("Addresses");
			return output;
		}

		public async Task<List<EmployerModel>> RetrieveAllEmployersAsync()
		{
			List<EmployerModel> output = await _database.RetrieveRecordsAsync<EmployerModel>("Employers");
			return output;
		}

		public async Task<List<PersonModel>> RetrieveAllPeopleAsync()
		{
			List<PersonModel> output = await _database.RetrieveRecordsAsync<PersonModel>("People");
			return output;
		}

		public async Task<EmployerModel> RetrieveEmployerByIdAsync(Guid id)
		{
			EmployerModel output = await _database.RetrieveRecordByIdAsync<EmployerModel>("Employers", id.ToString());
			return output;
		}

		public async Task<List<PersonModel>> RetrievePeopleByEmployerIdAsync(Guid employerId)
		{
			List<PersonModel> output = (await RetrieveAllPeopleAsync()).FindAll(x => x.Employer?.Id == employerId);
			return output;
		}

		public async Task<PersonModel> RetrievePersonByIdAsync(Guid id)
		{
			PersonModel output = await _database.RetrieveRecordByIdAsync<PersonModel>("People", id.ToString());
			return output;
		}

		public async Task UpdateAddressAsync(AddressModel address)
		{
			await _database.UpdateRecordAsync("Addresses", address);
		}

		public async Task UpdateEmployerAsync(EmployerModel employer)
		{
			await _database.UpdateRecordAsync("Employers", employer);
		}

		public async Task UpdatePersonAsync(PersonModel person)
		{
			await _database.UpdateRecordAsync("People", person);
		}
	}
}

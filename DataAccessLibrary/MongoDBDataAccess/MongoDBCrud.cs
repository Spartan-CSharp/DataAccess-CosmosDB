using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataAccessLibrary.Models;

using MongoDB.Driver;

namespace DataAccessLibrary.MongoDBDataAccess
{
	public class MongoDBCrud : ICrud
	{
		private readonly IMongoDatabase _mongoDatabase;

		public MongoDBCrud(string databaseName, string connectionString)
		{
			MongoClient client = new MongoClient(connectionString);
			_mongoDatabase = client.GetDatabase(databaseName);
		}

		public async Task CreateAddressAsync(AddressModel address)
		{
			await _mongoDatabase.CreateRecordAsync("Addresses", address);
		}

		public async Task CreateEmployerAsync(EmployerModel employer)
		{
			await _mongoDatabase.CreateRecordAsync("Employers", employer);
		}

		public async Task CreatePersonAsync(PersonModel person)
		{
			await _mongoDatabase.CreateRecordAsync("People", person);
		}

		public async Task DeleteAddressAsync(AddressModel address)
		{
			await _mongoDatabase.DeleteRecordAsync<AddressModel>("Addresses", address.Id);
		}

		public async Task DeleteEmployerAsync(EmployerModel employer)
		{
			await _mongoDatabase.DeleteRecordAsync<EmployerModel>("Employers", employer.Id);
		}

		public async Task DeletePersonAsync(PersonModel person)
		{
			await _mongoDatabase.DeleteRecordAsync<PersonModel>("People", person.Id);
		}

		public async Task<AddressModel> RetrieveAddressByIdAsync(Guid id)
		{
			AddressModel output = await _mongoDatabase.RetrieveRecordByIdAsync<AddressModel>("Addresses", id);
			return output;
		}

		public async Task<List<AddressModel>> RetrieveAllAddressesAsync()
		{
			List<AddressModel> output = await _mongoDatabase.RetrieveRecordsAsync<AddressModel>("Addresses");
			return output;
		}

		public async Task<List<EmployerModel>> RetrieveAllEmployersAsync()
		{
			List<EmployerModel> output = await _mongoDatabase.RetrieveRecordsAsync<EmployerModel>("Employers");
			return output;
		}

		public async Task<List<PersonModel>> RetrieveAllPeopleAsync()
		{
			List<PersonModel> output = await _mongoDatabase.RetrieveRecordsAsync<PersonModel>("People");
			return output;
		}

		public async Task<EmployerModel> RetrieveEmployerByIdAsync(Guid id)
		{
			EmployerModel output = await _mongoDatabase.RetrieveRecordByIdAsync<EmployerModel>("Employers", id);
			return output;
		}

		public async Task<List<PersonModel>> RetrievePeopleByEmployerIdAsync(Guid employerId)
		{
			List<PersonModel> output = (await RetrieveAllPeopleAsync()).FindAll(x => x.Employer?.Id == employerId);
			return output;
		}

		public async Task<PersonModel> RetrievePersonByIdAsync(Guid id)
		{
			PersonModel output = await _mongoDatabase.RetrieveRecordByIdAsync<PersonModel>("People", id);
			return output;
		}

		public async Task UpdateAddressAsync(AddressModel address)
		{
			await _mongoDatabase.UpdateRecordAsync("Addresses", address.Id, address);
		}

		public async Task UpdateEmployerAsync(EmployerModel employer)
		{
			await _mongoDatabase.UpdateRecordAsync("Employers", employer.Id, employer);
		}

		public async Task UpdatePersonAsync(PersonModel person)
		{
			await _mongoDatabase.UpdateRecordAsync("People", person.Id, person);
		}
	}
}

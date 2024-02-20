using System;
using System.Collections.Generic;

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

		public void CreateAddress(AddressModel address)
		{
			_mongoDatabase.CreateRecord("Addresses", address);
		}

		public void CreateEmployer(EmployerModel employer)
		{
			_mongoDatabase.CreateRecord("Employers", employer);
		}

		public void CreatePerson(PersonModel person)
		{
			_mongoDatabase.CreateRecord("People", person);
		}

		public void DeleteAddress(AddressModel address)
		{
			_mongoDatabase.DeleteRecord<AddressModel>("Addresses", address.Id);
		}

		public void DeleteEmployer(EmployerModel employer)
		{
			_mongoDatabase.DeleteRecord<EmployerModel>("Employers", employer.Id);
		}

		public void DeletePerson(PersonModel person)
		{
			_mongoDatabase.DeleteRecord<PersonModel>("People", person.Id);
		}

		public AddressModel RetrieveAddressById(Guid id)
		{
			AddressModel output = _mongoDatabase.RetrieveRecordById<AddressModel>("Addresses", id);
			return output;
		}

		public List<AddressModel> RetrieveAllAddresses()
		{
			List<AddressModel> output = _mongoDatabase.RetrieveRecords<AddressModel>("Addresses");
			return output;
		}

		public List<EmployerModel> RetrieveAllEmployers()
		{
			List<EmployerModel> output = _mongoDatabase.RetrieveRecords<EmployerModel>("Employers");
			return output;
		}

		public List<PersonModel> RetrieveAllPeople()
		{
			List<PersonModel> output = _mongoDatabase.RetrieveRecords<PersonModel>("People");
			return output;
		}

		public EmployerModel RetrieveEmployerById(Guid id)
		{
			EmployerModel output = _mongoDatabase.RetrieveRecordById<EmployerModel>("Employers", id);
			return output;
		}

		public List<PersonModel> RetrievePeopleByEmployerId(Guid employerId)
		{
			List<PersonModel> output = RetrieveAllPeople().FindAll(x => x.Employer?.Id == employerId);
			return output;
		}

		public PersonModel RetrievePersonById(Guid id)
		{
			PersonModel output = _mongoDatabase.RetrieveRecordById<PersonModel>("People", id);
			return output;
		}

		public void UpdateAddress(AddressModel address)
		{
			_mongoDatabase.UpdateRecord("Addresses", address.Id, address);
		}

		public void UpdateEmployer(EmployerModel employer)
		{
			_mongoDatabase.UpdateRecord("Employers", employer.Id, employer);
		}

		public void UpdatePerson(PersonModel person)
		{
			_mongoDatabase.UpdateRecord("People", person.Id, person);
		}
	}
}

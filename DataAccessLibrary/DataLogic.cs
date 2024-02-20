using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataAccessLibrary.CosmosDBDataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.MongoDBDataAccess;

using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary
{
	public class DataLogic : IDataLogic
	{
		private readonly IConfiguration _configuration;
		private readonly string _connectionString;
		private readonly ICrud _crud;

		public DataLogic(IConfiguration configuration, DBTYPES dbType, string databaseName)
		{
			_configuration = configuration;
			switch ( dbType )
			{
				case DBTYPES.MongoDB:
					_connectionString = _configuration.GetConnectionString("MongoDB");
					_crud = new MongoDBCrud(databaseName, _connectionString);
					break;
				case DBTYPES.CosmosDB:
					string endpointUri = _configuration.GetValue<string>("CosmosDB:EndpointUrl");
					string primaryKey = _configuration.GetValue<string>("CosmosDB:PrimaryKey");
					_crud = new CosmosDBCrud(endpointUri, primaryKey, databaseName);
					break;
				default:
					break;
			}
		}

		public async Task DeleteAddressAsync(AddressModel address)
		{
			List<PersonModel> allPeople = await GetAllPeopleAsync();
			foreach ( PersonModel person in allPeople )
			{
				foreach ( AddressModel personAddress in person.Addresses )
				{
					if ( personAddress.Id == address.Id )
					{
						_ = person.Addresses.Remove(personAddress);
						await UpdatePersonAsync(person);
					}
				}
			}

			List<EmployerModel> allEmployers = await GetAllEmployersAsync();
			foreach ( EmployerModel employer in allEmployers )
			{
				foreach ( AddressModel employerAddress in employer.Addresses )
				{
					if ( employerAddress.Id == address.Id )
					{
						_ = employer.Addresses.Remove(employerAddress);
						await UpdateEmployerAsync(employer);
					}
				}
			}

			await _crud.DeleteAddressAsync(address);
		}

		public async Task DeleteEmployerAsync(EmployerModel employer)
		{
			List<PersonModel> people = await _crud.RetrievePeopleByEmployerIdAsync(employer.Id);
			foreach ( PersonModel person in people )
			{
				person.Employer = null;
				await UpdatePersonAsync(person);
			}

			await _crud.DeleteEmployerAsync(employer);
		}

		public async Task DeletePersonAsync(PersonModel person)
		{
			await _crud.DeletePersonAsync(person);
		}

		public async Task<AddressModel> GetAddressByIdAsync(Guid addressId)
		{
			AddressModel output = await _crud.RetrieveAddressByIdAsync(addressId);
			return output;
		}

		public async Task<List<AddressModel>> GetAllAddressesAsync()
		{
			List<AddressModel> output = await _crud.RetrieveAllAddressesAsync();
			return output;
		}

		public async Task<List<EmployerModel>> GetAllEmployersAsync()
		{
			List<EmployerModel> output = await _crud.RetrieveAllEmployersAsync();
			return output;
		}

		public async Task<List<PersonModel>> GetAllPeopleAsync()
		{
			List<PersonModel> output = await _crud.RetrieveAllPeopleAsync();
			return output;
		}

		public async Task<EmployerModel> GetEmployerByIdAsync(Guid employerId)
		{
			EmployerModel output = await _crud.RetrieveEmployerByIdAsync(employerId);
			return output;
		}

		public async Task<PersonModel> GetPersonByIdAsync(Guid personId)
		{
			PersonModel output = await _crud.RetrievePersonByIdAsync(personId);
			return output;
		}

		public async Task SaveNewAddressAsync(AddressModel address)
		{
			await _crud.CreateAddressAsync(address);
		}

		public async Task SaveNewEmployerAsync(EmployerModel employer)
		{
			List<AddressModel> existingAddresses = await GetAllAddressesAsync();
			foreach ( AddressModel employerAddress in employer.Addresses )
			{
				if ( existingAddresses.Find(x => x.Id == employerAddress.Id) == null )
				{
					await SaveNewAddressAsync(employerAddress);
				}
			}

			await _crud.CreateEmployerAsync(employer);
		}

		public async Task SaveNewPersonAsync(PersonModel person)
		{
			if ( person.Employer != null )
			{
				EmployerModel existingEmployer = await GetEmployerByIdAsync(person.Employer.Id);
				if ( existingEmployer == null )
				{
					await SaveNewEmployerAsync(person.Employer);
				}
			}

			List<AddressModel> existingAddresses = await GetAllAddressesAsync();
			foreach ( AddressModel personAddress in person.Addresses )
			{
				if ( existingAddresses.Find(x => x.Id == personAddress.Id) == null )
				{
					await SaveNewAddressAsync(personAddress);
				}
			}

			await _crud.CreatePersonAsync(person);
		}

		public async Task UpdateAddressAsync(AddressModel address)
		{
			List<PersonModel> allPeople = await GetAllPeopleAsync();
			foreach ( PersonModel person in allPeople )
			{
				foreach ( AddressModel personAddress in person.Addresses )
				{
					if ( personAddress.Id == address.Id )
					{
						_ = person.Addresses.Remove(personAddress);
						person.Addresses.Add(address);
						await UpdatePersonAsync(person);
					}
				}
			}

			List<EmployerModel> allEmployers = await GetAllEmployersAsync();
			foreach ( EmployerModel employer in allEmployers )
			{
				foreach ( AddressModel employerAddress in employer.Addresses )
				{
					if ( employerAddress.Id == address.Id )
					{
						_ = employer.Addresses.Remove(employerAddress);
						employer.Addresses.Add(address);
						await UpdateEmployerAsync(employer);
					}
				}
			}

			await _crud.UpdateAddressAsync(address);
		}

		public async Task UpdateEmployerAsync(EmployerModel employer)
		{
			List<PersonModel> people = await _crud.RetrievePeopleByEmployerIdAsync(employer.Id);
			foreach ( PersonModel person in people )
			{
				person.Employer = employer;
				await UpdatePersonAsync(person);
			}

			await _crud.UpdateEmployerAsync(employer);
		}

		public async Task UpdatePersonAsync(PersonModel person)
		{
			await _crud.UpdatePersonAsync(person);
		}
	}
}

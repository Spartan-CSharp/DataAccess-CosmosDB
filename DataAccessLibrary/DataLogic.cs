using System;
using System.Collections.Generic;

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
				default:
					break;
			}
		}

		public void DeleteAddress(AddressModel address)
		{
			List<PersonModel> allPeople = GetAllPeople();
			foreach ( PersonModel person in allPeople )
			{
				foreach ( AddressModel personAddress in person.Addresses )
				{
					if ( personAddress.Id == address.Id )
					{
						_ = person.Addresses.Remove(personAddress);
						UpdatePerson(person);
					}
				}
			}

			List<EmployerModel> allEmployers = GetAllEmployers();
			foreach ( EmployerModel employer in allEmployers )
			{
				foreach ( AddressModel employerAddress in employer.Addresses )
				{
					if ( employerAddress.Id == address.Id )
					{
						_ = employer.Addresses.Remove(employerAddress);
						UpdateEmployer(employer);
					}
				}
			}

			_crud.DeleteAddress(address);
		}

		public void DeleteEmployer(EmployerModel employer)
		{
			List<PersonModel> people = _crud.RetrievePeopleByEmployerId(employer.Id);
			foreach ( PersonModel person in people )
			{
				person.Employer = null;
				UpdatePerson(person);
			}

			_crud.DeleteEmployer(employer);
		}

		public void DeletePerson(PersonModel person)
		{
			_crud.DeletePerson(person);
		}

		public AddressModel GetAddressById(Guid addressId)
		{
			AddressModel output = _crud.RetrieveAddressById(addressId);
			return output;
		}

		public List<AddressModel> GetAllAddresses()
		{
			List<AddressModel> output = _crud.RetrieveAllAddresses();
			return output;
		}

		public List<EmployerModel> GetAllEmployers()
		{
			List<EmployerModel> output = _crud.RetrieveAllEmployers();
			return output;
		}

		public List<PersonModel> GetAllPeople()
		{
			List<PersonModel> output = _crud.RetrieveAllPeople();
			return output;
		}

		public EmployerModel GetEmployerById(Guid employerId)
		{
			EmployerModel output = _crud.RetrieveEmployerById(employerId);
			return output;
		}

		public PersonModel GetPersonById(Guid personId)
		{
			PersonModel output = _crud.RetrievePersonById(personId);
			return output;
		}

		public void SaveNewAddress(AddressModel address)
		{
			_crud.CreateAddress(address);
		}

		public void SaveNewEmployer(EmployerModel employer)
		{
			List<AddressModel> existingAddresses = GetAllAddresses();
			foreach ( AddressModel employerAddress in employer.Addresses )
			{
				if ( existingAddresses.Find(x => x.Id == employerAddress.Id) == null )
				{
					SaveNewAddress(employerAddress);
				}
			}

			_crud.CreateEmployer(employer);
		}

		public void SaveNewPerson(PersonModel person)
		{
			if ( person.Employer != null )
			{
				EmployerModel existingEmployer = GetEmployerById(person.Employer.Id);
				if ( existingEmployer == null )
				{
					SaveNewEmployer(person.Employer);
				}
			}

			List<AddressModel> existingAddresses = GetAllAddresses();
			foreach ( AddressModel personAddress in person.Addresses )
			{
				if ( existingAddresses.Find(x => x.Id == personAddress.Id) == null )
				{
					SaveNewAddress(personAddress);
				}
			}

			_crud.CreatePerson(person);
		}

		public void UpdateAddress(AddressModel address)
		{
			List<PersonModel> allPeople = GetAllPeople();
			foreach ( PersonModel person in allPeople )
			{
				foreach ( AddressModel personAddress in person.Addresses )
				{
					if ( personAddress.Id == address.Id )
					{
						_ = person.Addresses.Remove(personAddress);
						person.Addresses.Add(address);
						UpdatePerson(person);
					}
				}
			}

			List<EmployerModel> allEmployers = GetAllEmployers();
			foreach ( EmployerModel employer in allEmployers )
			{
				foreach ( AddressModel employerAddress in employer.Addresses )
				{
					if ( employerAddress.Id == address.Id )
					{
						_ = employer.Addresses.Remove(employerAddress);
						employer.Addresses.Add(address);
						UpdateEmployer(employer);
					}
				}
			}

			_crud.UpdateAddress(address);
		}

		public void UpdateEmployer(EmployerModel employer)
		{
			List<PersonModel> people = _crud.RetrievePeopleByEmployerId(employer.Id);
			foreach ( PersonModel person in people )
			{
				person.Employer = employer;
				UpdatePerson(person);
			}

			_crud.UpdateEmployer(employer);
		}

		public void UpdatePerson(PersonModel person)
		{
			_crud.UpdatePerson(person);
		}
	}
}

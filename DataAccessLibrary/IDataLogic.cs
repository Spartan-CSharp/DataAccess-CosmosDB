using System;
using System.Collections.Generic;

using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
	public interface IDataLogic
	{
		void DeleteAddress(AddressModel address);
		void DeleteEmployer(EmployerModel employer);
		void DeletePerson(PersonModel person);
		AddressModel GetAddressById(Guid addressId);
		List<AddressModel> GetAllAddresses();
		List<EmployerModel> GetAllEmployers();
		List<PersonModel> GetAllPeople();
		EmployerModel GetEmployerById(Guid employerId);
		PersonModel GetPersonById(Guid personId);
		void SaveNewAddress(AddressModel address);
		void SaveNewEmployer(EmployerModel employer);
		void SaveNewPerson(PersonModel person);
		void UpdateAddress(AddressModel address);
		void UpdateEmployer(EmployerModel employer);
		void UpdatePerson(PersonModel person);
	}
}

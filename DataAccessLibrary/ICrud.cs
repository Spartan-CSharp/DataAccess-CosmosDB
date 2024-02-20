using System;
using System.Collections.Generic;

using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
	public interface ICrud
	{
		void CreateAddress(AddressModel address);
		void CreateEmployer(EmployerModel employer);
		void CreatePerson(PersonModel person);
		void DeleteAddress(AddressModel address);
		void DeleteEmployer(EmployerModel employer);
		void DeletePerson(PersonModel person);
		AddressModel RetrieveAddressById(Guid id);
		List<AddressModel> RetrieveAllAddresses();
		List<EmployerModel> RetrieveAllEmployers();
		List<PersonModel> RetrieveAllPeople();
		EmployerModel RetrieveEmployerById(Guid id);
		List<PersonModel> RetrievePeopleByEmployerId(Guid employerId);
		PersonModel RetrievePersonById(Guid id);
		void UpdateAddress(AddressModel address);
		void UpdateEmployer(EmployerModel employer);
		void UpdatePerson(PersonModel person);
	}
}

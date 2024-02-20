using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
	public interface ICrud
	{
		Task CreateAddressAsync(AddressModel address);
		Task CreateEmployerAsync(EmployerModel employer);
		Task CreatePersonAsync(PersonModel person);
		Task DeleteAddressAsync(AddressModel address);
		Task DeleteEmployerAsync(EmployerModel employer);
		Task DeletePersonAsync(PersonModel person);
		Task<AddressModel> RetrieveAddressByIdAsync(Guid id);
		Task<List<AddressModel>> RetrieveAllAddressesAsync();
		Task<List<EmployerModel>> RetrieveAllEmployersAsync();
		Task<List<PersonModel>> RetrieveAllPeopleAsync();
		Task<EmployerModel> RetrieveEmployerByIdAsync(Guid id);
		Task<List<PersonModel>> RetrievePeopleByEmployerIdAsync(Guid employerId);
		Task<PersonModel> RetrievePersonByIdAsync(Guid id);
		Task UpdateAddressAsync(AddressModel address);
		Task UpdateEmployerAsync(EmployerModel employer);
		Task UpdatePersonAsync(PersonModel person);
	}
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DataAccessLibrary.Models;

namespace DataAccessLibrary
{
	public interface IDataLogic
	{
		Task DeleteAddressAsync(AddressModel address);
		Task DeleteEmployerAsync(EmployerModel employer);
		Task DeletePersonAsync(PersonModel person);
		Task<AddressModel> GetAddressByIdAsync(Guid addressId);
		Task<List<AddressModel>> GetAllAddressesAsync();
		Task<List<EmployerModel>> GetAllEmployersAsync();
		Task<List<PersonModel>> GetAllPeopleAsync();
		Task<EmployerModel> GetEmployerByIdAsync(Guid employerId);
		Task<PersonModel> GetPersonByIdAsync(Guid personId);
		Task SaveNewAddressAsync(AddressModel address);
		Task SaveNewEmployerAsync(EmployerModel employer);
		Task SaveNewPersonAsync(PersonModel person);
		Task UpdateAddressAsync(AddressModel address);
		Task UpdateEmployerAsync(EmployerModel employer);
		Task UpdatePersonAsync(PersonModel person);
	}
}

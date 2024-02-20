using System;
using System.Collections.Generic;

using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLibrary.Models
{
	public class EmployerModel
	{
		[BsonId]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string CompanyName { get; set; }
		public List<AddressModel> Addresses { get; set; } = new List<AddressModel>();
	}
}

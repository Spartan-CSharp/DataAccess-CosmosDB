using System;

using MongoDB.Bson.Serialization.Attributes;

namespace DataAccessLibrary.Models
{
	public class AddressModel
	{
		[BsonId]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
	}
}

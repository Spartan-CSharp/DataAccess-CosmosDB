using System;

using MongoDB.Bson.Serialization.Attributes;

using Newtonsoft.Json;

namespace DataAccessLibrary.Models
{
	public class AddressModel
	{
		[BsonId]
		[JsonProperty(PropertyName = "id")]
		public Guid Id { get; set; } = Guid.NewGuid();
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string ZipCode { get; set; }
	}
}

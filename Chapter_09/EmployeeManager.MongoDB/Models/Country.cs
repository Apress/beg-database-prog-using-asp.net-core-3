using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace EmployeeManager.MongoDB.Models
{
    public class Country
    {
        [BsonId]
        public ObjectId DocumentID { get; set; }

        [BsonElement("countryID")]
        public int CountryID { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.MongoDB.Models
{
    public class AppUser
    {
        [BsonId]
        public ObjectId DocumentID { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }


        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("fullName")]
        public string FullName { get; set; }


        [BsonElement("birthDate")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime BirthDate { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }
    }
}

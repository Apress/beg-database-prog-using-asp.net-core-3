using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace EmployeeManager.CosmosDB.Models
{
    [Table("Users")]
    public class AppUser
    {
        [JsonProperty(PropertyName = "id")]
        public Guid DocumentID { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }


        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }


        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }


        [JsonProperty(PropertyName = "fullName")]
        public string FullName { get; set; }


        [JsonProperty(PropertyName = "birthDate")]
        public DateTime BirthDate { get; set; }


        [JsonProperty(PropertyName = "role")]
        public string Role { get; set; }
    }
}

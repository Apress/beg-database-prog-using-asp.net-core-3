using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace EmployeeManager.CosmosDB.Models
{
    public class Country
    {
        [JsonProperty(PropertyName = "id")]
        public Guid  DocumentID { get; set; }

        [JsonProperty(PropertyName = "countryID")]
        public int CountryID { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}

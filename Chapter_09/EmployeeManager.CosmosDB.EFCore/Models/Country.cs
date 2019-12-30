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
        [Key]
        public Guid  DocumentID { get; set; }

        public int CountryID { get; set; }

        public string Name { get; set; }
    }
}

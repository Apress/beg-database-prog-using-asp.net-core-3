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
        [Key]
        public Guid DocumentID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Role { get; set; }
    }
}

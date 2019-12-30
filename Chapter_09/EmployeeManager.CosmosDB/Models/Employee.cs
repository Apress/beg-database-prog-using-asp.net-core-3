using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace EmployeeManager.CosmosDB.Models
{

    public class Employee
    {
        [JsonProperty(PropertyName = "id")]
        public Guid DocumentID { get; set; }

        [Required]
        [Display(Name ="Employee ID")]
        [JsonProperty(PropertyName ="employeeID")]
        public int EmployeeID { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "First Name")]
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Title")]
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        [JsonProperty(PropertyName = "birthDate")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Hire Date")]
        [JsonProperty(PropertyName = "hireDate")]
        public DateTime HireDate { get; set; }


        [Required]
        [StringLength(15)]
        [Display(Name = "Country")]
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }



        [StringLength(500)]
        [Display(Name = "Notes")]
        [JsonProperty(PropertyName = "notes")]
        public string Notes { get; set; }
    }
}

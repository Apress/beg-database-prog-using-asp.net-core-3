using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;


namespace EmployeeManager.MongoDB.Models
{

    public class Employee
    {
        [BsonId]
        public ObjectId DocumentID { get; set; }


        [BsonElement("employeeID")]
        [Required]
        [Display(Name = "Employee ID")]
        public int EmployeeID { get; set; }

        [BsonElement("firstName")]
        [Required]
        [StringLength(10)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        [Required]
        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [BsonElement("title")]
        [Required]
        [StringLength(30)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [BsonElement("birthDate")]
        [Required]
        [Display(Name = "Birth Date")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime BirthDate { get; set; }

        [BsonElement("hireDate")]
        [Required]
        [Display(Name = "Hire Date")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime HireDate { get; set; }


        [BsonElement("country")]
        [Required]
        [StringLength(15)]
        [Display(Name = "Country")]
        public string Country { get; set; }



        [BsonElement("notes")]
        [StringLength(500)]
        [Display(Name = "Notes")]
        public string Notes { get; set; }
    }
}

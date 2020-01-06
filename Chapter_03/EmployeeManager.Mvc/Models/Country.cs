using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EmployeeManager.Mvc.Models
{
    [Table("Countries")]
    public class Country
    {
        [Column("EmployeeID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Country ID is required")]
        [Display(Name = "Country ID")]
        public int CountryID { get; set; }


        [Column("Name")]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(80, ErrorMessage = "Name must be less than 80 characters")]
        public string Name { get; set; }
    }
}

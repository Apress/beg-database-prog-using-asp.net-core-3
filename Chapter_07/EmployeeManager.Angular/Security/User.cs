using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EmployeeManager.Angular.Security
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "User ID")]
        public int UserID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "Role")]
        public string Role { get; set; }

    }
}

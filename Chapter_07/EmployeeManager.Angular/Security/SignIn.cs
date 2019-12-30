using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManager.Angular.Security
{
    public class SignIn
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }
    }
}

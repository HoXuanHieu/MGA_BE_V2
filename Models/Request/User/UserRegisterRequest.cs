using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Request
{
    public class UserRegisterRequest
    {
        [Required]
        [StringLength(100)]
        public String UserName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        [StringLength(100)]
        //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")]
        public String Password { get; set; }

        [Required]
        [RegularExpression(@"^(admin|reader|poster)$")]
        public String Role { get; set; }
    }
}

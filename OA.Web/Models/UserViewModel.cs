using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OA.Web.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress) , Required]
        public string Email { get; set; }
        
        [DataType(DataType.Password) , Required]
        public string Password { get; set; }
        public string ErrorMsg { get; set; }
    }
}

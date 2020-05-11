using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashbackDomain.Identity
{
    public class User : IdentityUser<int>
    {
        
        [Column(TypeName = "nvarchar(150)")]
        override
        public string UserName { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}

using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Project.Domain.UserAgg.Entities
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

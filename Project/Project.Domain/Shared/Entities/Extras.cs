using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Project.Domain.Shared.Entities
{
    public static class Extras
    {
        public static HashSet<string> GetErrorsDescription(this IdentityResult identityResult)
        {
            return identityResult.Errors.Select(error => error.Description).ToHashSet();
        }
    }
}
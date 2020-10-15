using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Project.Domain.MovieAgg.Entities;
using Project.Domain.UserAgg.Entities;

namespace Project.Domain.UserAgg
{
    public class User : IdentityUser<int>
    {
        public bool Active { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Introduction { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }

        public User() { }

        public User(string email, string firstName, string lastName)
        {
            Active = true;
            UserName = email;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Created = DateTime.Now;
            Updated = DateTime.Now;
        }

        public static void Update(User user, string firstName, string lastName, string introduction)
        {
            user.FirstName = firstName;
            user.LastName = lastName;
            user.Introduction = introduction;
        }
        public static void Disable(User user)
        {
            user.Active = false;
        }

        public static bool IsValid(User user)
        {
            return user.Active;
        }
    }
}

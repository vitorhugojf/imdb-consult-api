using System;

namespace Project.Domain.DTO.User
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public virtual string Introduction { get; set; }
    }
}

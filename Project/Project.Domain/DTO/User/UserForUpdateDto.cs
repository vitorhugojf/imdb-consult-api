namespace Project.Domain.DTO.User
{
    public class UserForUpdateDto
    {
        public int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public string Introduction { get; set; }
    }
}

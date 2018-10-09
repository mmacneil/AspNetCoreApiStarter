using Web.Api.Core.Shared;


namespace Web.Api.Core.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; private set; } // EF migrations require at least private setter - won't work on auto-property
        public string LastName { get; private set; }
        public string IdentityId { get; private set; }
        public string Email { get; }
        public string UserName { get; }
        public string PasswordHash { get; }

        internal User() { /* Appease EF */ }

        internal User(string firstName, string lastName, string email, string userName, string identityId, string passwordHash = null)
        {
            IdentityId = identityId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = userName;
            PasswordHash = passwordHash;
        }
    }
}

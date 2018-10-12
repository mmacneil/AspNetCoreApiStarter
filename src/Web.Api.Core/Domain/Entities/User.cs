using System;
using System.Collections.Generic;
using Web.Api.Core.Shared;


namespace Web.Api.Core.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; private set; } // EF migrations require at least private setter - won't work on auto-property
        public string LastName { get; private set; }
        public string IdentityId { get; private set; }
        public string UserName { get; private set; } // Required by automapper
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();

        internal User() { /* Required by EF */ }

        internal User(string firstName, string lastName, string identityId)
        {
            FirstName = firstName;
            LastName = lastName;
            IdentityId = identityId;
        }

        public void AddRereshToken(string token,int userId,string remoteIpAddress)
        {
            _refreshTokens.Add(new RefreshToken(token, DateTime.UtcNow.AddDays(5),userId, remoteIpAddress));
        }
    }
}

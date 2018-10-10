using System;
using Web.Api.Core.Shared;


namespace Web.Api.Core.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; private set; }
        public DateTime Expires { get; private set; }
        public int UserId { get; private set; }
        public bool Active { get; private set; }

        public RefreshToken(string token, DateTime expires,int userId)
        {
            Token = token;
            Expires = expires;
            UserId = userId;
        }

        public void Deactivate()
        {
            Active = false;
        }
    }
}

using System;
using Web.Api.Core.Shared;


namespace Web.Api.Core.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}

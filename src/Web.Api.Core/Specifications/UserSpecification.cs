using Web.Api.Core.Domain.Entities;

namespace Web.Api.Core.Specifications
{
    public sealed class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(string userName) : base(u => u.UserName==userName)
        {
        }
    }
}

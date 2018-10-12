using Web.Api.Core.Domain.Entities;
using Web.Api.Infrastructure.Data;
using Web.Api.Infrastructure.Identity;


namespace Web.Api.IntegrationTests
{
    public static class SeedData
    {
        public static void PopulateTestData(AppIdentityDbContext dbContext)
        {
            dbContext.Users.Add(new AppUser
            {
                Id = "c5e2143c-b764-43bf-9b73-b21ca2e63931",
                UserName = "mmacneil",
                NormalizedUserName = "MMACNEIL",
                Email = "mark@fullstackmark.com",
                NormalizedEmail = "MARK@FULLSTACKMARK.COM",
                PasswordHash = "AQAAAAEAACcQAAAAEBPy+7xDLL61sRc6y6OWbzayNInAwaRROPuoaoUq78ecEB0iHhZGUy2+smMWuuyzYg==",
                SecurityStamp = "7TQD5YWQO4SR2AWN4LCKHI3U4QRZD6ON",
                ConcurrencyStamp = "4a6b983d-0019-4624-811d-f8c7ffa7762a"
            });
            
            dbContext.SaveChanges();
        }

        public static void PopulateTestData(AppDbContext dbContext)
        {
            dbContext.Users.Add(new User("Mark", "Macneil", "c5e2143c-b764-43bf-9b73-b21ca2e63931"));
            dbContext.SaveChanges();
        }
    }
}

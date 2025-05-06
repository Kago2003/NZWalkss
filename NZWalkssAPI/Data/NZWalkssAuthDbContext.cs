using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalkssAPI.Data
{
    public class NZWalkssAuthDbContext : IdentityDbContext
    {
        public NZWalkssAuthDbContext(DbContextOptions <NZWalkssAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "aab2bc7d-8ec7-468a-a52c-71bbfa71fdad";
            var writerRoleId = "666f6c9a-8680-4ddb-b764-ecc5e19ae94e";

            //Creting a list of roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },

                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            //Seed into the builder object
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}

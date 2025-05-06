using Microsoft.EntityFrameworkCore;
using NZWalkssAPI.Models.Domain;

namespace NZWalkssAPI.Data
{
    public class NZWalkssDbContext : DbContext
    {
        public NZWalkssDbContext(DbContextOptions <NZWalkssDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Reagions { get; set; }
        public DbSet<Walks> Walks { get; set; }
        public DbSet<Image> Images { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seeding the data for difficulties
            //easy , medium and hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("0266074e-cc21-4a13-8d8a-24f0e85ccb6c"),
                    Name = "Easy"
                },

                new Difficulty()
                {

                    Id = Guid.Parse("3f82955a-5375-422c-9e29-077ef19d53d3"),
                    Name = "Medium"
                },

                new Difficulty()
                {
                    Id = Guid.Parse("283568c0-9ab1-47c6-a884-bb889b167750"),
                    Name = "Hard"
                }
            };

            //seed difficulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seeding the data for regions


            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("272cfc74-77f0-49eb-aad6-b11283cdaa0d"),
                    Name = "Sandton",
                    Code = "SDT",
                    RegionImageURL = "https://th.bing.com/th/id/OIP.MI2hagCeMic8FNoH32HnCgAAAA?rs=1&pid=ImgDetMain"
                },

                new Region()
                {

                    Id = Guid.Parse("fed490ce-1f12-4e1e-8aea-160ffcfea7e4"),
                    Name = "Midrand",
                    Code = "MRD",
                    RegionImageURL = "https://www.sa-venues.com/maps/atlas/gau_midrand.gif"
                },

                new Region()
                {
                    Id = Guid.Parse("02c2fae2-2957-4fb5-8ddf-f67b90bc129d"),
                    Name = "Roodeport",
                    Code = "RDP",
                    RegionImageURL = "https://www.sa-venues.com/maps/atlas/gau_roodepoort.gif"
                }
            };

            //seed Regions to the database
            modelBuilder.Entity<Region>().HasData(regions);
        }

    }
 }


using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Model;

namespace PeopleManager.Core
{
    public class PeopleManagerDbContext: IdentityDbContext
    {
        public PeopleManagerDbContext(DbContextOptions<PeopleManagerDbContext> options): base(options)
        {
            
        }

        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<Person> People => Set<Person>();
       
        public void Seed()
        {
            //Add Identity User for testing
            var user = new IdentityUser
            {
                UserName = "bavo.ketels@vives.be",
                NormalizedUserName = "BAVO.KETELS@VIVES.BE",
                PasswordHash = "AQAAAAIAAYagAAAAECU5deQZ/RgRC30HxRSj0h4/uRxKhuNGVnBADrSsmcN+xxw8RRDEgc74k9xJqr2bqA==", //Test123$
                ConcurrencyStamp = "60442557-5b88-4c02-8c47-b0e9f1ddb4de",
                SecurityStamp = "HS5B2LRJZ2ZHJT7WKKG6MMWMEHP24KVO"
            };

            Users.Add(user);

            People.AddRange(new List<Person>
            {
                new Person { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
                new Person { FirstName = "Jane", LastName = "Smith", Email = null },
                new Person { FirstName = "Bob", LastName = "Johnson", Email = "bob.j@example.com" },
                new Person { FirstName = "Alice", LastName = "Brown", Email = "alice.b@example.com" },
                new Person { FirstName = "Eva", LastName = "Wilson", Email = null },
                new Person { FirstName = "Michael", LastName = "Clark", Email = "michael.c@example.com" },
                new Person { FirstName = "Olivia", LastName = "Taylor", Email = "olivia.t@example.com" },
                new Person { FirstName = "Daniel", LastName = "Anderson", Email = null },
                new Person { FirstName = "Sophia", LastName = "Lee", Email = "sophia.l@example.com" },
                new Person { FirstName = "David", LastName = "Davis", Email = null }
            });

            SaveChanges();
        }
    }
}

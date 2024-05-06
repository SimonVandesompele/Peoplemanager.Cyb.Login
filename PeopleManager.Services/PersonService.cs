using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Model;
using PeopleManager.Services.Model.Requests;
using PeopleManager.Services.Model.Results;

namespace PeopleManager.Services
{
    public class PersonService(PeopleManagerDbContext dbContext)
    {
        public async Task<IList<PersonResult>> Find()
        {
            return await dbContext.People
                .Select(p => new PersonResult
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    NumberOfAssignments = p.Assignments.Count
                })
                .ToListAsync();
        }

        public async Task<PersonResult?> Get(int id)
        {
            return await dbContext.People
                .Select(p => new PersonResult
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    NumberOfAssignments = p.Assignments.Count
                })
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PersonResult?> Create(PersonRequest request)
        {
            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };

            dbContext.People.Add(person);
            await dbContext.SaveChangesAsync();

            return await Get(person.Id);
        }

        public async Task<PersonResult?> Update(int id, PersonRequest request)
        {
            var person = await dbContext.People.FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return null;
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;

            await dbContext.SaveChangesAsync();

            return await Get(id);
        }

        public async Task Delete(int id)
        {
            var person = await dbContext.People.FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return;
            }

            dbContext.People.Remove(person);

            await dbContext.SaveChangesAsync();
        }
    }
}

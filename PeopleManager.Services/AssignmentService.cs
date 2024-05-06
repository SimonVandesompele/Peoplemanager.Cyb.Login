
using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Model;
using PeopleManager.Services.Model.Requests;
using PeopleManager.Services.Model.Results;

namespace PeopleManager.Services
{
    public class AssignmentService(PeopleManagerDbContext dbContext)
    {
        public async Task<IList<AssignmentResult>> Find()
        {
            return await dbContext.Assignments
                .Include(a => a.AssignedTo)
                .Select(a => new AssignmentResult
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    AssignedToId = a.AssignedToId,
                    AssignedToFirstName = a.AssignedTo != null ? a.AssignedTo.FirstName : null,
                    AssignedToLastName = a.AssignedTo != null ? a.AssignedTo.LastName : null
                })
                .ToListAsync();
        }

        public async Task<AssignmentResult?> Get(int id)
        {
            return await dbContext.Assignments
                .Include(a => a.AssignedTo)
                .Select(a => new AssignmentResult
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    AssignedToId = a.AssignedToId,
                    AssignedToFirstName = a.AssignedTo != null ? a.AssignedTo.FirstName : null,
                    AssignedToLastName = a.AssignedTo != null ? a.AssignedTo.LastName : null
                })
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<AssignmentResult?> Create(AssignmentRequest request)
        {
            var assignment = new Assignment
            {
                Name = request.Name,
                Description = request.Description,
                AssignedToId = request.AssignedToId
            };
            dbContext.Assignments.Add(assignment);
            await dbContext.SaveChangesAsync();

            return await Get(assignment.Id);
        }

        public async Task<AssignmentResult?> Update(int id, AssignmentRequest request)
        {
            var assignment = await dbContext.Assignments
                .FirstOrDefaultAsync(p => p.Id == id);

            if (assignment is null)
            {
                return null;
            }

            assignment.Name = request.Name;
            assignment.Description = request.Description;
            assignment.AssignedToId = request.AssignedToId;

            await dbContext.SaveChangesAsync();

            return await Get(id);
        }

        public async Task Delete(int id)
        {
            var assignment = await dbContext.Assignments.FirstOrDefaultAsync(p => p.Id == id);

            if (assignment is null)
            {
                return;
            }

            dbContext.Assignments.Remove(assignment);

            await dbContext.SaveChangesAsync();
        }
    }
}

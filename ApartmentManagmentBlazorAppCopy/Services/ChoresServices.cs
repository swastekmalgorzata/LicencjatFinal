using System.Security.Claims;
using ApartmentManagmentBlazorAppCopy.Models;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagmentBlazorAppCopy.Services
{
    public interface IChoresService
    {
        IList<Chore>? GetChores(long apartmentId);
        Chore GetChore(long id, long apartmentId);
        Task<long> AddChore(Chore chore, long apartmentId);
        Task MarkChore(long id, bool isDone, string? userId);
        Task UpdateChore(Chore updatedChore, long id, long apartmentId);
        Task DeleteChore(long apartmentId, long id);
        IList<Chore>? GetNotDoneChores(long apartmentId);
    }
    public class ChoresServices : IChoresService
    {
        private readonly ApartmentsContext dbContext;
        public ChoresServices(ApartmentsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<Chore>? GetNotDoneChores(long apartmentId)
        {
            return dbContext.Chores.Where(c => c.ApartmentId == apartmentId && c.IsDone == false).ToList();
        }

        public IList<Chore>? GetChores(long apartmentId)
        {
            return dbContext.Chores.Where(c => c.ApartmentId == apartmentId).ToList() ?? null;
        }

        public Chore GetChore(long id, long apartmentId) 
        {
            var chore = dbContext.Chores.FirstOrDefault(c => c.Id == id && c.ApartmentId == apartmentId);
            if (chore == null)
            {
                throw new Exception("Not found");
            }
            return chore;
        }

        public async Task<long> AddChore(Chore chore, long apartmentId)
        {
            chore.ApartmentId = apartmentId;
            chore.IsDone = false;
            dbContext.Add(chore);
            await dbContext.SaveChangesAsync();

            return chore.Id;
        }
        public async Task MarkChore(long id, bool isDone, string? userId)
        {
            var existingChore = dbContext.Chores.FirstOrDefault(c => c.Id == id);

            if (existingChore is null)
            {
                throw new Exception("Not found");
            }
            existingChore.IsDone = isDone;
            existingChore.UserId = userId;

            await dbContext.SaveChangesAsync();
        }
        public async Task UpdateChore(Chore updatedChore, long id, long apartmentId)
        {
            //var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserId");
            var existingChore = dbContext.Chores.FirstOrDefault(c => c.Id == id && c.ApartmentId == apartmentId);
            if (existingChore is null)
            {
                throw new Exception("Not found");
            }

            existingChore.ChoreName = updatedChore.ChoreName;
            //existingChore.UserId = updatedChore.UserId;
            existingChore.IsDone = updatedChore.IsDone;
            await dbContext.SaveChangesAsync();
            
        }

        public async Task DeleteChore(long apartmentId, long id)
        {
            var chore = dbContext.Chores.FirstOrDefault(c => c.Id == id);
            if (chore is null)
            {
                throw new Exception("Not found");
            }

            dbContext.Remove(chore);
            await dbContext.SaveChangesAsync();

        }
    }
}

using System.Security.Claims;
using ApartmentManagmentBlazorAppCopy.Data;
using ApartmentManagmentBlazorAppCopy.Models;
using ApartmentManagmentBlazorAppCopy.Requests.ApartmentRequests;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagmentBlazorAppCopy.Services
{
    public interface IRommateServices
    {
        IList<ApplicationUser> GetRoommatesUserInfo(long apartmentId);
        ApplicationUser GetUserApllicationInfo(string userId);
        Task<Roommate> AddRommate(long apartmentId, IEnumerable<Claim> claims);
        Task UpdateRommate(long apartmentId, long roommateId, AddRoommateRequest request);
        Task DeleteRommate(long apartmentId, string userId);
        long? GetUserApartmentId(string userId);
        IList<Roommate> GetRoommates(long apartmentId);
        Task ChangeRommateName(long apartmentID, string userId, string userName);
    }
    public class RommateServices : IRommateServices
    {
        private readonly ApartmentsContext dbContext;
        private readonly UserContext userContext;
        public RommateServices(ApartmentsContext dbContext, UserContext userContext)
        {
            this.dbContext = dbContext;
            this.userContext = userContext;
        }
        public async Task<Roommate> AddRommate(long apartmentId, IEnumerable<Claim> claims)
        {
            var userId = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value;
            var userName = claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")!.Value;

            if (dbContext.Apartments.FirstOrDefault(a => a.Id == apartmentId) is null)
            {
                throw new Exception("Apartment not found");
            };
            
            var rommate = new Roommate()
            {
                ApartmentId = apartmentId,
                UserId = userId!,
                Name = userName
            };
            
            dbContext.Add(rommate);
            await dbContext.SaveChangesAsync();

            return rommate;
        }
        
        public async Task UpdateRommate(long apartmentId, long roommateId, AddRoommateRequest request)
        {
            var existingRoommate = await dbContext.Roommates
                .FirstOrDefaultAsync(r => r.ApartmentId == apartmentId && r.RoommateId == roommateId);

            if (existingRoommate == null)
            {
                throw new Exception("Roommate not found");

            }

            existingRoommate.Name = request.Name;

            await dbContext.SaveChangesAsync();
        }
        
        public async Task DeleteRommate(long apartmentId, string userId)
        {
            var roommate = dbContext.Roommates.FirstOrDefault(r => r.UserId == userId);
            if (roommate is null) throw new Exception("Not found");
            dbContext.Remove(roommate);
            await dbContext.SaveChangesAsync();
        }
        public IList<ApplicationUser> GetRoommatesUserInfo(long apartmentId)
        {
            var usersIds = dbContext.Roommates.Where(r => r.ApartmentId == apartmentId);
            if (usersIds is null) throw new Exception("Not found");

            var users = userContext.Users.Where(u => !usersIds.Any(ui => ui.UserId.ToString() == u.Id)).ToList();
            if (users is null) throw new Exception("Not found");

            return users!;
        }
        public ApplicationUser GetUserApllicationInfo(string userId)
        {
            var user= userContext.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null) throw new Exception("Not found");

            return user!;
        }
        public IList<Roommate> GetRoommates(long apartmentId)
        {
            return dbContext.Roommates.Where(a => a.ApartmentId == apartmentId).ToList();

        }

        public async Task ChangeRommateName(long apartmentID, string userId, string userName)
        {
            var user = await dbContext.Roommates.FirstOrDefaultAsync(r => r.Name == userName);
            if (user is not null)
            {
                user.Name = userName;
                await dbContext.SaveChangesAsync();
            }
        }

        public long? GetUserApartmentId(string userId)
        {
            return dbContext.Roommates.SingleOrDefault(r => r.UserId == userId)?.ApartmentId;
        }
    }
}

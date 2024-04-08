using ApartmentManagmentBlazorAppCopy.Models;
using ApartmentManagmentBlazorAppCopy.Requests.ApartmentRequests;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagmentBlazorAppCopy.Services
{
    public interface IRentServices
    {
        Task<Rent> AddRent(long apartmentId, IList<Roommate> Rommates, AddRentRequest request);
        Task UpdateRent(long apartmentId, AddRentRequest request, IList<Roommate> Rommates, long rentId);
        IList<Rent> GetRents(long apartmentId);
        Task DeleteRent(long apartmentId, long rentId);
        Task MarkIsPaid(long rentId, string name, bool isPaid);
        Rent GetRent(long rentId, long apartmentId);
        IList<Rent> GetAprroachingDeadline(long apartmentId);
        IList<RoommatePart>? GetToPay(long ApartmentId, string userId);
    }

    public class RentServices : IRentServices
    {
        private readonly ApartmentsContext apartmentsContext;

        public RentServices(ApartmentsContext apartmentsContext)
        {
            this.apartmentsContext = apartmentsContext;
        }
        public IList<RoommatePart>? GetToPay(long ApartmentId,string userId)
        {
            var test = apartmentsContext.Rents.FirstOrDefault(a => a.ApartmentId == ApartmentId)?.CustomCostBreakDown
                ?.ToList();
            return test?.Where(t => t.UserId == userId && t.IsPaid == false).ToList();
        }
        public IList<Rent> GetAprroachingDeadline(long apartmentId)
        {
            var date = DateTimeOffset.Now.AddDays(14);
            var date2 = new DateOnly(date.Year, date.Month, date.Day);
            return apartmentsContext.Rents.Where(r => r.ApartmentId == apartmentId && r.DeadLine >= date2).ToList();
        } 
        public async Task<Rent> AddRent(long apartmentId, IList<Roommate> Rommates, AddRentRequest request)
        {
            if (request.CustomBreakdown is false)
            {
                request.CustomCostBreakDown = new List<RoommatePart>();
                foreach (var roommate in Rommates)
                {
                    request.CustomCostBreakDown.Add(new RoommatePart()
                    {
                        IsPaid = false,
                        Share = request.WholeAmount / Rommates.Count(),
                        UserId = roommate.UserId,
                        RoommateName = roommate.Name
                    });
                }
            }
            var newRent = new Rent()
            {
                ApartmentId = apartmentId,
                WholeAmount = request.WholeAmount,
                CustomBreakdown = request.CustomBreakdown,
                Month = request.Month,
                Year = request.Year,
                CustomCostBreakDown = request.CustomCostBreakDown,
                DeadLine = request.DeadLine,
                EndDate = request.EndDate
            };

            apartmentsContext.Rents.Add(newRent);
            await apartmentsContext.SaveChangesAsync();

            return newRent;
        }

        public async Task UpdateRent(long apartmentId, AddRentRequest request, IList<Roommate> Rommates, long rentId)
        {
            var existingRent = await apartmentsContext.Rents
                .FirstOrDefaultAsync(r => r.ApartmentId == apartmentId && r.RentId
                    == rentId);

            if (existingRent == null)
            {
                throw new Exception("Not found");
            }
            
            if (request.CustomBreakdown is false)
            {
                request.CustomCostBreakDown = new List<RoommatePart>();
                foreach (var roommate in Rommates)
                {
                    request.CustomCostBreakDown.Add(new RoommatePart()
                    {
                        IsPaid = false,
                        Share = request.WholeAmount / Rommates.Count(),
                        UserId = roommate.UserId
                    });
                }
            }
            existingRent.WholeAmount = request.WholeAmount;
            existingRent.CustomBreakdown = request.CustomBreakdown;
            existingRent.Month = request.Month;
            existingRent.Year = request.Year;
            existingRent.CustomCostBreakDown = request.CustomCostBreakDown;
            existingRent.DeadLine = request.DeadLine;
            existingRent.EndDate = request.EndDate;

            await apartmentsContext.SaveChangesAsync();
        }

        public IList<Rent> GetRents(long apartmentId)
        {
            return apartmentsContext.Rents.Where(r => r.ApartmentId == apartmentId).ToList();
        }

        public async Task DeleteRent(long apartmentId, long rentId)
        {
            var rent = apartmentsContext.Rents.FirstOrDefault(r => r.RentId == rentId);
            if (rent is null) throw new Exception("Not found");
            apartmentsContext.Remove(rent);
            await apartmentsContext.SaveChangesAsync();
        }

        public async Task MarkIsPaid(long rentId, string name, bool isPaid)
        {
            var rent = apartmentsContext.Rents.FirstOrDefault(r => r.RentId == rentId);
            if (rent is null || rent.CustomCostBreakDown is null) throw new Exception("Rent error");
            
            rent.CustomCostBreakDown!.FirstOrDefault(cb => cb.RoommateName == name)!.IsPaid = isPaid;
            await apartmentsContext.SaveChangesAsync();
        }

        public Rent GetRent(long rentId, long apartmentId)
        {
            var rent = apartmentsContext.Rents.FirstOrDefault(r => r.RentId == rentId);
            if (rent is null) throw new Exception("Rent error");
            return rent;
        }
    } 
}

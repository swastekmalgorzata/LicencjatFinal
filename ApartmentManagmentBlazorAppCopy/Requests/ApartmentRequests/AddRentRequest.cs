using ApartmentManagmentBlazorAppCopy.Models;

namespace ApartmentManagmentBlazorAppCopy.Requests.ApartmentRequests
{
    public class AddRentRequest
    {
        public double WholeAmount { get; set; }
        public bool CustomBreakdown { get; set; }
        public string Month { get; set; } = null!;
        public string Year { get; set; } = null!;
        public ICollection<RoommatePart>? CustomCostBreakDown { get; set; }
        public double? EvenBreakDown { get; set; }
        public DateOnly DeadLine { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}

using static ApartmentManagmentBlazorAppCopy.Models.Meter;

namespace ApartmentManagmentBlazorAppCopy.Requests.ApartmentRequests
{
    public class AddMeterRequest
    {
        public double GasReading { get; set; }
        public double WaterReading { get; set; }
        public double EnergyReading { get; set; }
        public string Month { get; set; } = null!;
    }
}

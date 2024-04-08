using ApartmentManagmentBlazorAppCopy.Models;
using ApartmentManagmentBlazorAppCopy.Requests.ApartmentRequests;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagmentBlazorAppCopy.Services
{ 
    public interface IMeterServices
    {
        Task<long> AddMeter(long apartmentId, AddMeterRequest request);
        Task UpdateMeter(long apartmentId, long meterId, AddMeterRequest request);
        Task DeleteMeter(long apartmentId, long meterId);
        IList<Meter> GetMeters(long apartmentId);
        Task<Meter> GetMeter(long meterId, long apartmentId);
    }
    public class MeterServices : IMeterServices
    {
        private readonly ApartmentsContext _dbContext;

        public MeterServices(ApartmentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> AddMeter(long apartmentId, AddMeterRequest request)
        {
            var meter = new Meter()
            {
                ApartmentId = apartmentId,
                WaterReading = request.WaterReading,
                GasReading = request.GasReading,
                EnrgyReading = request.EnergyReading,
                Month = request.Month,
            };
            _dbContext.Add(meter);
            await _dbContext.SaveChangesAsync();
            return meter.Id;
        }
        public async Task UpdateMeter(long apartmentId, long meterId, AddMeterRequest request)
        {
            var existingMeter = await _dbContext.Meters
                .FirstOrDefaultAsync(m => m.ApartmentId == apartmentId && m.Id == meterId);

            if (existingMeter == null)
            {
                throw new Exception("Not found");
            }

            existingMeter.GasReading = request.GasReading;
            existingMeter.WaterReading = request.EnergyReading;
            existingMeter.EnrgyReading = request.EnergyReading;
            existingMeter.Month = request.Month;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteMeter(long apartmentId, long meterId)
        {
            var meter = _dbContext.Meters.FirstOrDefault(r => r.Id == meterId);
            if (meter is null) throw new Exception("Not found");
            _dbContext.Remove(meter);
            await _dbContext.SaveChangesAsync();
        }

        public IList<Meter> GetMeters(long apartmentId)
        {
            var meters = _dbContext.Meters.Where(r => r.ApartmentId == apartmentId);
            if (meters is null) throw new Exception("Not found");

            return meters.ToList();
        }

        public async Task<Meter> GetMeter(long meterId, long apartmentId)
        {
            var meter = await _dbContext.Meters.SingleOrDefaultAsync(r => r.ApartmentId == apartmentId && r.Id == meterId);
            if (meter is null) throw new Exception("Not found");

            return meter;
        }
    }
}

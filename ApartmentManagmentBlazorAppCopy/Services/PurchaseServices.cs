using System.Collections;
using ApartmentManagmentBlazorAppCopy.Models;

namespace ApartmentManagmentBlazorAppCopy.Services
{
    public interface IPurchaseServices
    {
        IList<PurchaseRecord> GetPurchaseRecords(long apartmentId);
        PurchaseRecord GetPurchaseRecord(long id, long apartmentId);
        Task MarkItemBought(long purchaseId, bool isBought, string? userId);
        Task<long> AddPurchase(PurchaseRecord purchaseRecord);
        Task UpdatePurchase(long apartmentId, PurchaseRecord updatedPurchaseRecord, long id);
        Task DletePurchase(long apartmentId, long id);
        IList<PurchaseRecord> GetPendingPurchases(long apartmentId);
    }
    public class PurchaseServices : IPurchaseServices
    {
        private readonly ApartmentsContext dbContext;
        public PurchaseServices(ApartmentsContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<PurchaseRecord> GetPendingPurchases(long apartmentId)
        {
            return dbContext.PurchaseRecords.Where(p => p.ApartmentId == apartmentId && p.IsBought == false).ToList();
        }
        public IList<PurchaseRecord> GetPurchaseRecords(long apartmentId)
        {
            return dbContext.PurchaseRecords.Where(p => p.ApartmentId == apartmentId).ToList(); 
        }

        public PurchaseRecord GetPurchaseRecord(long id, long apartmentId)
        {
            var purchaseRecord = dbContext.PurchaseRecords.FirstOrDefault(pr => pr.Id == id && pr.ApartmentId == apartmentId);

            if (purchaseRecord == null)
            {
                throw new Exception("Not found");
            }

            return purchaseRecord!;
        }

        public async Task<long> AddPurchase(PurchaseRecord purchaseRecord)
        {
            dbContext.Add(purchaseRecord);
            await dbContext.SaveChangesAsync();

            return purchaseRecord.Id;
        }

        public async Task UpdatePurchase(long apartmentId, PurchaseRecord updatedPurchaseRecord, long id)
        {
            var existingPurchaseRecord = dbContext.PurchaseRecords.FirstOrDefault(pr => pr.Id == id && pr.ApartmentId == apartmentId);
            if (existingPurchaseRecord is null)
            {
                throw new Exception("Not found");
            }

            existingPurchaseRecord.ItemName = updatedPurchaseRecord.ItemName;
            existingPurchaseRecord.Cost = updatedPurchaseRecord.Cost;
            existingPurchaseRecord.UserId = updatedPurchaseRecord.UserId;
            existingPurchaseRecord.IsBought = updatedPurchaseRecord.IsBought;

            await dbContext.SaveChangesAsync();

        }

        public async Task DletePurchase(long apartmentId, long id)
        {
            var purchaseRecord = dbContext.PurchaseRecords.FirstOrDefault(pr => pr.Id == id);
            if (purchaseRecord is null)
            {
                throw new Exception("Not found");
            }

            dbContext.Remove(purchaseRecord);
            await dbContext.SaveChangesAsync();
        }
        
        public async Task MarkItemBought(long purchaseId, bool isBought, string? userId)
        {
            var purchase = dbContext.PurchaseRecords.FirstOrDefault(p => p.Id == purchaseId);
            if (purchase is not null)
            {
                purchase.IsBought = isBought;

                purchase.UserId = userId;
                await dbContext.SaveChangesAsync();
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ApartmentManagmentBlazorAppCopy.Models;
using ApartmentManagmentBlazorAppCopy.Requests.ApartmentRequests;

namespace ApartmentManagmentBlazorAppCopy
{
    public static class ApartmentModule
    {
        public static void AddApartmentEnpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/apartment", async (ApartmentsContext dbContext) =>
            {
                var test = dbContext.Add(new Apartment());
                await dbContext.SaveChangesAsync();
                return test.Entity.Id;
            });
            app.MapGet("/apartment/{id}", async (ApartmentsContext dbContext, long id) =>
            {
                var test = dbContext.Add(new Apartment());
                await dbContext.SaveChangesAsync();
                return test;
            });

        }
        public static void AddRentEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/apartment/{apartmentId}/rent", async (ApartmentsContext dbContext, long apartmentId, AddRentRequest request) =>
            {
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
                dbContext.Add(newRent);
                await dbContext.SaveChangesAsync();
                return Results.Created("https://localhost:44329/apartment/{apartmentId}/rents", newRent);
            });
            app.MapPut("/apartment/{apartmentId}/rent/{rentId}", async (ApartmentsContext dbContext, long apartmentId, AddRentRequest request, long rentId) =>
            {
                var existingRent = await dbContext.Rents
                    .FirstOrDefaultAsync(r => r.ApartmentId == apartmentId && r.RentId
                    == rentId);

                if (existingRent == null)
                {
                    return Results.NotFound($"Rent entry with ID {rentId} for Apartment ID {apartmentId} not found.");
                }

                existingRent.WholeAmount = request.WholeAmount;
                existingRent.CustomBreakdown = request.CustomBreakdown;
                existingRent.Month = request.Month;
                existingRent.Year = request.Year;
                existingRent.CustomCostBreakDown = request.CustomCostBreakDown;
                existingRent.DeadLine = request.DeadLine;
                existingRent.EndDate = request.EndDate;

                // Save changes to the database
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });
            app.MapGet("/apartment/{apartmentId}/rents", (ApartmentsContext dbContext, long apartmentId) =>
            {
                return dbContext.Rents.Select(r => r.ApartmentId == apartmentId);
            });
            app.MapDelete("/apartment/{apartmentId}/rent/{id}", async (ApartmentsContext dbContext, long apartmentId, long rentId) =>
            {
                var rent = dbContext.Rents.FirstOrDefault(r => r.RentId == rentId);
                if (rent is null) throw new Exception("Not found");
                dbContext.Remove(rent);
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            });
        }
        public static void AddExploitationCostsEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/apartment/{apartmentId}/exploitationCost", async (ApartmentsContext dbContext, long apartmentId, AddExploitationCostRequest request) =>
            {
                var newExploitationCost = new ExploitationCost()
                {
                    ApartmentId = apartmentId,
                    Type = request.Type,
                    WholeAmount = request.WholeAmount,
                    CustomBreakdown = request.CustomBreakdown,
                    Month = request.Month,
                    Year = request.Year,
                    CustomCostBreakDown = request.CustomCostBreakDown,
                    EvenBreakDown = request.EvenBreakDown,
                    DeadLine = request.DeadLine,
                    EndDate = request.EndDate
                };
                dbContext.Add(newExploitationCost);
                await dbContext.SaveChangesAsync();
                return Results.Created("https://localhost:44329/apartment/{apartmentId}/exploitationCosts", newExploitationCost);
            });
            app.MapPut("/apartment/{apartmentId}/exploitationCost/{exploitationCostId}", async (ApartmentsContext dbContext, long apartmentId, long exploitationCostId, AddExploitationCostRequest request) =>
            {
                var existingExploitationCost = await dbContext.ExploitationCosts
                    .FirstOrDefaultAsync(ec => ec.ApartmentId == apartmentId && ec.Id == exploitationCostId);

                if (existingExploitationCost == null)
                {
                    return Results.NotFound($"Exploitation cost entry with ID {exploitationCostId} for Apartment ID {apartmentId} not found.");
                }

                existingExploitationCost.Type = request.Type;
                existingExploitationCost.WholeAmount = request.WholeAmount;
                existingExploitationCost.CustomBreakdown = request.CustomBreakdown;
                existingExploitationCost.Month = request.Month;
                existingExploitationCost.Year = request.Year;
                existingExploitationCost.CustomCostBreakDown = request.CustomCostBreakDown;
                existingExploitationCost.EvenBreakDown = request.EvenBreakDown;
                existingExploitationCost.DeadLine = request.DeadLine;
                existingExploitationCost.EndDate = request.EndDate;

                // Save changes to the database
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });
            app.MapGet("/apartment/{apartmentId}/exploitationCosts", (ApartmentsContext dbContext, long apartmentId) =>
            {
                return dbContext.ExploitationCosts.Select(r => r.ApartmentId == apartmentId);
            });
            app.MapDelete("/apartment/{apartmentId}/exploitationCosts/{id}", async (ApartmentsContext dbContext, long apartmentId, long exploitationCostId) =>
            {
                var rent = dbContext.ExploitationCosts.FirstOrDefault(r => r.Id == exploitationCostId);
                if (rent is null) throw new Exception("Not found");
                dbContext.Remove(rent);
                await dbContext.SaveChangesAsync();
                return Results.NoContent();
            });
        }
        public static void AddRoomateEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/apartment/{apartmentId}/roommate", async (ApartmentsContext dbContext, long apartmentId, AddRoommateRequest request ) =>
            {
                var rommate = new Roommate()
                {
                    ApartmentId = apartmentId,
                    UserId = request.UserId.ToString()
                };
                dbContext.Add(rommate);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            });
            app.MapPut("/apartment/{apartmentId}/roommate/{roommateId}", async (ApartmentsContext dbContext, long apartmentId, long roommateId, AddRoommateRequest request) =>
            {
                var existingRoommate = await dbContext.Roommates
                    .FirstOrDefaultAsync(r => r.ApartmentId == apartmentId && r.RoommateId == roommateId);

                if (existingRoommate == null)
                {
                    return Results.NotFound($"Roommate entry with ID {roommateId} for Apartment ID {apartmentId} not found.");
                }

                existingRoommate.Name = request.Name;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });
            app.MapDelete("/apartment/{apartmentId}/roommate/{userId}", async (ApartmentsContext dbContext, long apartmentId, Guid userId) =>
            {
                var roommate = dbContext.Roommates.FirstOrDefault(r => r.UserId == userId.ToString());
                if (roommate is null) throw new Exception("Not found");
                dbContext.Remove(roommate);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });
            app.MapGet("/apartment/{apartmentId}/roommates",  (ApartmentsContext dbContext, UserContext userContext,long apartmentId) =>
            {
                var usersIds = (dbContext.Roommates.Where(r => r.ApartmentId == apartmentId)).Select(x => x.UserId);
                if (usersIds is null) throw new Exception("Not found");
                var users = userContext.Users;
                
                return users.ToList();
            });
        }
        public static void AddMeterEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/apartment/{apartmentId}/meter", async (ApartmentsContext dbContext, long apartmentId, AddMeterRequest request) =>
            {
                var meter = new Meter()
                {
                    ApartmentId = apartmentId,
                    WaterReading = request.WaterReading,
                    GasReading = request.GasReading,
                    EnrgyReading = request.EnergyReading,
                    Month = request.Month,
                };
                dbContext.Add(meter);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            });
            app.MapPut("/apartment/{apartmentId}/meter/{meterId}", async (ApartmentsContext dbContext, long apartmentId, long meterId, AddMeterRequest request) =>
            {
                var existingMeter = await dbContext.Meters
                    .FirstOrDefaultAsync(m => m.ApartmentId == apartmentId && m.Id == meterId);

                if (existingMeter == null)
                {
                    return Results.NotFound($"Meter entry with ID {meterId} for Apartment ID {apartmentId} not found.");
                }

                existingMeter.GasReading = request.GasReading;
                existingMeter.WaterReading = request.EnergyReading;
                existingMeter.EnrgyReading = request.EnergyReading;
                existingMeter.Month = request.Month;

                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });
            app.MapDelete("/apartment/{apartmentId}/meter/{meterId}", async (ApartmentsContext dbContext, long apartmentId, long meterId) =>
            {
                var meter = dbContext.Meters.FirstOrDefault(r => r.Id == meterId);
                if (meter is null) throw new Exception("Not found");
                dbContext.Remove(meter);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });
            app.MapGet("/apartment/{apartmentId}/meters",  (ApartmentsContext dbContext, long apartmentId) =>
            {
                var meters = dbContext.Meters.Where(r => r.ApartmentId == apartmentId);
                if (meters is null) throw new Exception("Not found");
                    
                return meters.ToList();
            });
        }
        public static void AddPurchaseEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/apartment/{apartmentId}/purchaseRecords", (ApartmentsContext dbContext, long apartmentId) => dbContext.PurchaseRecords.Where(p=> p.ApartmentId == apartmentId));

            app.MapGet("/apartment/{apartmentId}/purchaseRecords/{id}", (int id, ApartmentsContext dbContext, long apartmentId) =>
            {
                var purchaseRecord = dbContext.PurchaseRecords.FirstOrDefault(pr => pr.Id == id && pr.ApartmentId == apartmentId);
                return purchaseRecord is not null ? Results.Ok(purchaseRecord) : Results.NotFound();
            });

            app.MapPost("/apartment/{apartmentId}/purchaseRecords", (ApartmentsContext dbContext, PurchaseRecord purchaseRecord) =>
            {
                dbContext.Add(purchaseRecord);
                return Results.Created($"/purchaseRecords/{purchaseRecord.Id}", purchaseRecord);
            });

            app.MapPut("/apartment/{apartmentId}/purchaseRecords/{id}", (ApartmentsContext dbContext, long apartmentId, PurchaseRecord updatedPurchaseRecord, long id) =>
            {
                var existingPurchaseRecord = dbContext.PurchaseRecords.FirstOrDefault(pr => pr.Id == id && pr.ApartmentId == apartmentId);
                if (existingPurchaseRecord is null)
                {
                    return Results.NotFound();
                }

                existingPurchaseRecord.ItemName = updatedPurchaseRecord.ItemName;
                existingPurchaseRecord.Cost = updatedPurchaseRecord.Cost;
                existingPurchaseRecord.UserId = updatedPurchaseRecord.UserId;
                existingPurchaseRecord.IsBought = updatedPurchaseRecord.IsBought;

                return Results.Ok(existingPurchaseRecord);
            });

            app.MapDelete("/apartment/{apartmentId}/purchaseRecords/{id}", (ApartmentsContext dbContext, long apartmentId, long id) =>
            {
                var purchaseRecord = dbContext.PurchaseRecords.FirstOrDefault(pr => pr.Id == id);
                if (purchaseRecord is null)
                {
                    return Results.NotFound();
                }

                dbContext.Remove(purchaseRecord);
                return Results.NoContent();
            });
        }
        public static void AddChoresEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/apartment/{apartmentId}/chores", (ApartmentsContext dbContext, long apartmentId) =>
                dbContext.Chores.Where(c => c.ApartmentId == apartmentId));

            app.MapGet("/apartment/{apartmentId}/chores/{id}", (int id, ApartmentsContext dbContext, long apartmentId) =>
            {
                var chore = dbContext.Chores.FirstOrDefault(c => c.Id == id && c.ApartmentId == apartmentId);
                return chore is not null ? Results.Ok(chore) : Results.NotFound();
            });

            app.MapPost("/apartment/{apartmentId}/chores", (ApartmentsContext dbContext, Chore chore) =>
            {
                dbContext.Add(chore);
                return Results.Created($"/chores/{chore.Id}", chore);
            });
            app.MapPut("/apartment/{apartmentId}/chores/{id}/markAsDone", (ClaimsPrincipal claimsPrincipal, ApartmentsContext dbContext, Chore chore, long id) => 
            { 
                var existingChore = dbContext.Chores.FirstOrDefault(c => c.Id == id);
                var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserId");

                if (existingChore is null)
                {
                    return Results.NotFound();
                }
                existingChore.IsDone = true;
                existingChore.UserId = userId!.Value;
                return Results.Ok(existingChore);

            });

            app.MapPut("/apartment/{apartmentId}/chores/{id}", (ClaimsPrincipal claimsPrincipal, ApartmentsContext dbContext, long apartmentId, Chore updatedChore, long id) =>
            {
                var userId = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "UserId");
                var existingChore = dbContext.Chores.FirstOrDefault(c => c.Id == id && c.ApartmentId == apartmentId);
                if (existingChore is null)
                {
                    return Results.NotFound();
                }

                existingChore.ChoreName = updatedChore.ChoreName;
                existingChore.UserId = updatedChore.UserId;
                existingChore.IsDone = updatedChore.IsDone;

                return Results.Ok(existingChore);
            });

            app.MapDelete("/apartment/{apartmentId}/chores/{id}", (ApartmentsContext dbContext, long apartmentId, long id) =>
            {
                var chore = dbContext.Chores.FirstOrDefault(c => c.Id == id);
                if (chore is null)
                {
                    return Results.NotFound();
                }

                dbContext.Remove(chore);
                return Results.NoContent();
            });
        }
         
        //public static void AddFileEndpoints(this IEndpointRouteBuilder app)
        //{
        //    app.MapGet("/apartment/{apartmentId}/documents", async (ApartmentsContext dbContext, long apartmentId) =>
        //    {
        //        var files = dbContext.Files.Where(r => r.ApartmentId == apartmentId);
        //        if (files is null) throw new Exception("Not found");

        //        foreach (var file in files)
        //        {
        //            var content = new System.IO.MemoryStream(file.DocByte);
        //            var path = Path.Combine(
        //               Directory.GetCurrentDirectory(), "FileDownloaded",
        //               file.FileName);

        //            await CopyStream(content, path);
        //        }


        //        return Results.Ok();
        //    });
        //    async Task CopyStream(Stream stream, string downloadPath)
        //    {
        //        using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
        //        {
        //            await stream.CopyToAsync(fileStream);
        //        }
        //    }
        //    app.MapPost("/apartment/{apartmentId}/documents", async (ApartmentsContext dbContext, long apartmentId, [FromBody] IFormFile file) =>
        //    {
        //        var document = new Models.File()
        //        {
        //            ApartmentId = apartmentId,
        //            FileName = file.FileName,
        //            FileType = file.ContentType
        //        };
        //        using (var _MemoryStream = new MemoryStream())
        //        {
        //            file.CopyTo(_MemoryStream);
        //            document.DocByte = _MemoryStream.ToArray();
        //        }

        //        dbContext.Add(document);

        //        await dbContext.SaveChangesAsync();

        //        return Results.Ok();
        //    });
        //    app.MapPut("/apartment/{apartmentId}/documents/{documentId}", async (ApartmentsContext dbContext, long apartmentId, long documentId, AddFileRequest request) =>
        //    {
        //        var existingDocument = await dbContext.Files
        //            .FirstOrDefaultAsync(f => f.ApartmentId == apartmentId && f.Id == documentId);

        //        if (existingDocument == null)
        //        {
        //            return Results.NotFound($"Document with ID {documentId} for Apartment ID {apartmentId} not found.");
        //        }

        //        existingDocument.FileName = request.Files.FileName;
        //        existingDocument.FileType = request.Files.ContentType;

        //        using (var _MemoryStream = new MemoryStream())
        //        {
        //            request.Files.CopyTo(_MemoryStream);
        //            existingDocument.DocByte = _MemoryStream.ToArray();
        //        }

        //        await dbContext.SaveChangesAsync();

        //        return Results.NoContent();
        //    });
        //    app.MapDelete("/apartment/{apartmentId}/document/{documentId}", async (ApartmentsContext dbContext, long apartmentId, long documentId) =>
        //    {
        //        var document = dbContext.Files.FirstOrDefault(r => r.Id == documentId);
        //        if (document is null) throw new Exception("Not found");
        //        dbContext.Remove(document);
        //        await dbContext.SaveChangesAsync(); 

        //        return Results.NoContent();
        //    });
        //}
    }
}

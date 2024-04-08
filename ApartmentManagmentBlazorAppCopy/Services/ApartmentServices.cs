using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentManagmentBlazorAppCopy.Services
{
    public interface IApartmentServices
    {
        Task<long> AddApartment(string name);
        Apartment GetApartment(long id);
    }
    public class ApartmentServices : IApartmentServices
    {
        private readonly ApartmentsContext apartmentsContext;
        public ApartmentServices(ApartmentsContext apartmentsContext)
        {
            this.apartmentsContext = apartmentsContext;
        }

        public async Task<long> AddApartment(string name)
        {
            var test = apartmentsContext.Add(new Apartment(){ Name = name});
            await apartmentsContext.SaveChangesAsync();
            return test.Entity.Id;
        }
        public Apartment GetApartment(long id)
        {
            var apartment = apartmentsContext.Apartments.SingleOrDefault(a => a.Id == id);
            if (apartment is null)
            {
                throw new Exception("Not found");
            }
            return apartment!;
        }
    }
}

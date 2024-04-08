using ApartmentManagmentBlazorAppCopy.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagmentBlazorAppCopy
{
    public class UserContext(DbContextOptions<UserContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApartmentManagmentBlazorAppCopy.Models
{
    [Owned]
    public class RoommatePart
    { 
        [ForeignKey("AspNetUsers")]
        public string UserId { get; set; }
        public string? RoommateName { get; set; }
        public double Share { get; set; }
        public bool IsPaid { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ApartmentManagmentBlazorAppCopy.Models;

namespace ApartmentManagmentBlazorAppCopy.Models
{
    public class Rent
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RentId { get; set; }
        [ForeignKey("Apartment")]
        public long ApartmentId { get; set; }
        public double WholeAmount { get; set; }
        public bool CustomBreakdown { get; set; } 
        public string Month { get; set; } = null!;
        public string Year { get; set; } = null!;
        public ICollection<RoommatePart>? CustomCostBreakDown { get; set; }
        public DateOnly DeadLine { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public double? EvenBreakDown { get; set; }

    }
}

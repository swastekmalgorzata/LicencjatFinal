using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentManagmentBlazorAppCopy.Models
{
    public class PurchaseRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string ItemName { get; set; } = null!;
        public double? Cost { get; set; }
        [ForeignKey("AspNetUsers")]
        public string? UserId { get; set; } //userId from token?
        public bool IsBought { get; set; }
        [ForeignKey("Apartment")]
        public long ApartmentId { get; set; }
    }
}

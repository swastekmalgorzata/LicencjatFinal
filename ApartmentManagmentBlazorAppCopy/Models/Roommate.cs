using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentManagmentBlazorAppCopy.Models
{
    public class Roommate
    {
        [ForeignKey("AspNetUsers")]
        [Key]
        public string UserId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RoommateId { get; set; }
        [ForeignKey("Apartment")]
        public long ApartmentId { get; set; }
        public string? Name { get; set; }
    }
}

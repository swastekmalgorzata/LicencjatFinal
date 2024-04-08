using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentManagmentBlazorAppCopy.Models
{
    public class Chore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string ChoreName { get; set; } = null!;
        [ForeignKey("AspNetUsers")]
        public string? UserId { get; set; } //userId from token?
        public bool IsDone { get; set; }
        [ForeignKey("Apartment")]
        public long ApartmentId { get; set; }
        public string? NameOfDoer { get; set; }
    }
}

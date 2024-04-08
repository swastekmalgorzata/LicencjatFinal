using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentManagmentBlazorAppCopy.Models
{
    public class Meter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [ForeignKey("Apartment")]
        public long ApartmentId { get; set; }
        public double EnrgyReading { get; set; }
        public double GasReading { get; set; }
        public double WaterReading { get; set; }
        public string Month { get; set; } = null!;
    }
}

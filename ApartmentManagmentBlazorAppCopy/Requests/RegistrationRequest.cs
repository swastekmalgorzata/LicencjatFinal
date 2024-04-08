using System.ComponentModel.DataAnnotations;

namespace ApartmentManagmentBlazorAppCopy.Requests
{
    public class RegistrationRequest
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;

    }
}

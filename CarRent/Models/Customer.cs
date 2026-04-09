using System.ComponentModel.DataAnnotations;

namespace CarRent.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        // Validation annotation #1 & #2
        [Required]
        [StringLength(50)]
        public string FullName { get; set; } = string.Empty;

        // Validation annotation #3
        [EmailAddress]
        public string? Email { get; set; }

        // One-to-Many (Customer -> Rentals)
        public ICollection<Rental>? Rentals { get; set; }

        // Many-to-Many (Customer <-> Car)
        public ICollection<CustomerCar>? CustomerCars { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CarRent.Models
{

    public class Car
    {
        public int CarId { get; set; }


        [Required]
        public string Brand { get; set; } = string.Empty;

        [Required]
        public string Model { get; set; } = string.Empty;

        public int Year { get; set; }

        public ERentalType RentalType { get; set; } = ERentalType.Daily;

        [Range(1, 10000)]
        public double PricePerDay { get; set; }

        
        public IEnumerable<Rental>? Rentals { get; set; }

        public ICollection<CustomerCar> CustomerCars { get; set; } = new List<CustomerCar>();

    }
}
    
namespace CarRent.Models
{
    public class CustomerCar
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}

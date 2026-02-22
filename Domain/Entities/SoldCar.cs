using System.ComponentModel.DataAnnotations;

namespace myapp.Domain.Entities;

public class SoldCar
{
    public int Id { get; set; }

    [Required]
    public int CarId { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Range(0.01, 1000000000)]
    public decimal Price { get; set; }

    public Car? Car { get; set; }
    public Customer? Customer { get; set; }
}

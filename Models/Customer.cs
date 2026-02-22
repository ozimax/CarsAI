using System.ComponentModel.DataAnnotations;

namespace myapp.Models;

public class Customer
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(200)]
    public string Email { get; set; } = string.Empty;

    public ICollection<SoldCar> SoldCars { get; set; } = new List<SoldCar>();
}

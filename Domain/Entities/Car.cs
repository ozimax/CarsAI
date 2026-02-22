using System.ComponentModel.DataAnnotations;

namespace myapp.Domain.Entities;

public class Car
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Make { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Model { get; set; } = string.Empty;

    [Range(1886, 3000)]
    public int Year { get; set; }

    [Required]
    [StringLength(100)]
    public string DriverName { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string PlateNumber { get; set; } = string.Empty;

    public ICollection<SoldCar> SoldCars { get; set; } = new List<SoldCar>();
}

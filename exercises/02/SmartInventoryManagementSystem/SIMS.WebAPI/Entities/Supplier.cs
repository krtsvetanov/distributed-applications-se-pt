using System.ComponentModel.DataAnnotations;

namespace SIMS.WebAPI.Entities;

public class Supplier
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 5)]
    public decimal Rating { get; set; }

    [Required]
    [MaxLength(100)]
    public string Country { get; set; } = string.Empty;

    // Navigation properties
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

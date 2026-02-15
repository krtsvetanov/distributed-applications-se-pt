using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMS.WebAPI.Entities;

public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string SKU { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public int StockQuantity { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public int SupplierId { get; set; }

    // Navigation properties
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey(nameof(SupplierId))]
    public virtual Supplier Supplier { get; set; } = null!;

    public virtual ICollection<StockMovement> StockMovements { get; set; } = new List<StockMovement>();
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SIMS.WebAPI.Entities;

public class StockMovement
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    [MaxLength(10)]
    public string Type { get; set; } = string.Empty; // "In" or "Out"

    [Required]
    public int Quantity { get; set; }

    [Required]
    public DateTime Date { get; set; }

    // Navigation properties
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; } = null!;
}

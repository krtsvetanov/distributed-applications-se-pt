using System.ComponentModel.DataAnnotations;

namespace SIMS.WebAPI.Entities;

public class Warehouse
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    [Required]
    public int Capacity { get; set; }
}

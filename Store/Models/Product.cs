using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Store.Models;

namespace Store.models;

class Product
{
    [Key]
    public int ProductID { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(50)]
    public string Brand { get; set; }

    [Required]
    [StringLength(255)]
    public string Description { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock must be positive or 0")]
    [DefaultValue(0)]
    public string Stock { get; set; }

    [Required]
    public int PriceID { get; set; }

    public Price Price { get; set; }
}
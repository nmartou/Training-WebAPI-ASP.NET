using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Price
{
    [Key]
    public int PriceID { get; set; }

    [Required]
    [Range(0.0d, double.MaxValue, ErrorMessage = "Price out of range")]
    [Column(TypeName = "money")]
    public decimal Value { get; set; }

    [Required]
    public string Currency { get; set; } = "USD";

    [Required]
    [DefaultValue(0.0f)]
    [Range(0.0f, 100.0f, ErrorMessage = "Discount is out of range")]
    public float Discount { get; set; }

    [Column(TypeName = "DateTime")]
    public DateTime? DiscountEndDate { get; set; }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

class Price
{
    [Key]
    public int PriceID { get; set; }

    [Required]
    [Range(0.0d, double.MaxValue, ErrorMessage = "Price out of range")]
    [Column(TypeName = "money")]
    public decimal Value { get; set; }

    [Required]
    [DefaultValue(0.0f)]
    [Range(0.0f, 100.0f, ErrorMessage = "Discount is out of range")]
    public float discount { get; set; }

    [Required]
    public DateTime DiscountEndDate { get; set; } = System.DateTime.Now;
}
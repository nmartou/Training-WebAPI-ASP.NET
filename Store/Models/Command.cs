using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Command
{
    [Key]
    public int CommandID { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity of product should be positive and not equal to zero")]
    public int TotalQuanity { get; set; }

    [Required]
    [Range(0.0d, double.MaxValue, ErrorMessage = "Total price shoul be positive or zero")]
    [DefaultValue(0.0d)]
    [Column(TypeName = "money")]
    public decimal TotalPrice { get; set; }

    [Required]
    [DefaultValue(false)]
    public bool IsPaid { get; set; }

    [Required]
    [ForeignKey("Seller")]
    public int SellerID { get; set; }
    public Person? Seller { get; set; }

    [Required]
    [ForeignKey("Buyer")]
    public int BuyerID { get; set; }
    public Person? Buyer { get; set; }

    // public int InvoiceID { get; set; } // Theorical foreign key nullable

    // public Invoice Invoice { get; set; }
}
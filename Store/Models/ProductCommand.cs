using System.ComponentModel.DataAnnotations;
using Store.models;

namespace Store.Models;

class ProductCommand
{
    [Key]
    public int ProductCommandID { get; set; }

    [Required]
    public int CommandID { get; set; }

    [Required]
    public int ProductID { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity is not greater than zero")]
    public int Quantity { get; set; }

    public Command Command { get; set; }
    public Product Product { get; set; }
}
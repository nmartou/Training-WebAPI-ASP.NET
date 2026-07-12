using System.ComponentModel.DataAnnotations;

namespace Store.Models;

public class Person
{
    [Key]
    public int PersonID { get; set; }

    [Required]
    [StringLength(30)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; }

    [Required]
    public DateOnly BirthDate { get; set; }
}
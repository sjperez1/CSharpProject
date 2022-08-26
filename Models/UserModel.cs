#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CSharpProject.Models;

public class User 
{
    [Key]
    public int userId { get; set; }


    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least two chars")]
    [Display(Name = "First Name")]
    public string firstName { get; set; }


    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least two chars")]
    [Display(Name = "Last Name")]
    public string lastName { get; set; }


    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least two chars")]
    [Display(Name = "Username")]
    public string userName { get; set; }


    [Required(ErrorMessage = "is required")]
    [EmailAddress(ErrorMessage = "must be a valid email")]
    [Display(Name = "Email")]
    public string email { get; set; }


    [Required(ErrorMessage = "is required")]
    [MinLength(8, ErrorMessage = "must be at least 8 chars")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string password { get; set; }


    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("password", ErrorMessage = "doesn't match password")]
    [Display(Name = "Confirm Password")]
    public string confirmpass { get; set; }
    public DateTime created_at { get; set; } = DateTime.Now;
    public DateTime updated_at { get; set; } = DateTime.Now;
}
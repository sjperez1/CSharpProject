#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CSharpProject.Models;
public class List
{
    [Key]
    public int ListId {get;set;}

    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least 2 characters long")]
    [Display(Name = "List Title")]
    public string Title {get;set;}

    [Required(ErrorMessage = "is required")]
    [MinLength(2, ErrorMessage = "must be at least 5 characters long")]
    [Display(Name = "Description")]
    public string Description {get;set;}

    [Required(ErrorMessage = "is required")]
    public string AddFilm {get;set;}

    public int WatchTime {get;set;}

    // public string Invite {get;set;}

    [Display(Name = "Marathon Date (optional)")]
    [DataType(DataType.Date)]
    [FutureDate]
    public DateTime MarathonDate {get;set;}

    public DateTime CreatedAt {get;set;} = DateTime.Now;

    public DateTime UpdatedAt {get;set;} = DateTime.Now;

    public int UserId {get;set;}
    public User? ListCreator {get;set;}
}

public class FutureDateAttribute : ValidationAttribute
{
    // Need the question marks to account for any possible null values
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // the following if statement will help pass the validator so that it continues to the next step because you cannot check something that is empty
        if(value == null)
        {
            return ValidationResult.Success;
        }

        DateTime date = (DateTime)value;

        if(date <= DateTime.Now)
        {
            return new ValidationResult("must be in the future");
        }

        return ValidationResult.Success;
    }
}
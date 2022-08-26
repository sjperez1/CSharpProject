#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpProject.Models;

public class Movie
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public string image_url { get; set; }
    public string wiki { get; set; }
    public DateTime Created_At { get; set; }
    public DateTime Updated_At { get; set; }
}
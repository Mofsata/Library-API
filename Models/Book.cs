using System.ComponentModel.DataAnnotations;

namespace MyLibrary.Models;

public class Book{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Author { get; set; }
    public int PublishYear { get; set; }
    public string? Edition { get; set; }

}
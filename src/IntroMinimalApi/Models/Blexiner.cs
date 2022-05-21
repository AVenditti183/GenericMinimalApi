namespace IntroMinimalApi.Models;
public class Blexiner
{
    public Guid Id { get; set; }
    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(30)]
    public string LastName { get; set; }
    public string JobTitle { get; set; }
}

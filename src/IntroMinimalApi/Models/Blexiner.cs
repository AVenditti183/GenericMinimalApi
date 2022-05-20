namespace IntroMinimalApi.Models;
public class Blexiner
{
    public Blexiner()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
}

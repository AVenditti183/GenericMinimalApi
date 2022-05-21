using IntroMinimalApi.Models;
using Entities = IntroMinimalApi.Data.Entities;

namespace IntroMinimalApi.Extensions;
public static class EntityExtensions
{
    public static Blexiner ToModel(this Entities.Blexiner blexiner)
    {
        return new Blexiner
        {
            Id = blexiner.Id,
            FirstName = blexiner.FirstName,
            LastName = blexiner.LastName,
            JobTitle = blexiner.JobTitle
        };
    }
}

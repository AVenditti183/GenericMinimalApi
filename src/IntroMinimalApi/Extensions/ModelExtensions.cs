using IntroMinimalApi.Models;
using Entities = IntroMinimalApi.Data.Entities;

namespace IntroMinimalApi.Extensions
{
    public static class ModelExtensions
    {
        public static Entities.Blexiner ToEntity (this Blexiner blexiner) 
        {
            return new Entities.Blexiner
            {
                FirstName = blexiner.FirstName,
                LastName = blexiner.LastName,
                JobTitle = blexiner.JobTitle
            };
        }
    }
}

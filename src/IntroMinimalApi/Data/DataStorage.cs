using IntroMinimalApi.Data.Entities;

namespace IntroMinimalApi.Data
{
    public class DataStorage
    {
        public List<Blexiner> Blexiners { get; set; }
        
        public DataStorage()
        {
            Blexiners = new List<Blexiner>
            {
                new Blexiner { FirstName = "Michele", LastName = "Aponte", JobTitle = "CEO" },
                new Blexiner { FirstName = "Antonio", LastName = "Liccardi", JobTitle = "CTO" },
                new Blexiner { FirstName = "Antonio", LastName = "Venditti", JobTitle = "Architect" },
                new Blexiner { FirstName = "Francesco", LastName = "De Vicariis", JobTitle = "Architect" },
                new Blexiner { FirstName = "Gaetano", LastName = "Paternò", JobTitle = "Architect" },
                new Blexiner { FirstName = "Marco", LastName = "Savarese", JobTitle = "Architect" },
                new Blexiner { FirstName = "Adolfo", LastName = "Arnold", JobTitle = "Senior Developer" },
                new Blexiner { FirstName = "Enrico", LastName = "Bencivenga", JobTitle = "Senior Developer" },
                new Blexiner { FirstName = "Genny", LastName = "Paudice", JobTitle = "Senior Developer" },
                new Blexiner { FirstName = "Anna Maria", LastName = "Serra", JobTitle = "Senior Developer" },
                new Blexiner { FirstName = "Antonio", LastName = "Tammaro", JobTitle = "Senior Developer" },
                new Blexiner { FirstName = "Francesco", LastName = "Vastarella", JobTitle = "Senior Developer" },
                new Blexiner { FirstName = "Gerardo", LastName = "Greco", JobTitle = "Senior Developer" }
            };
        }
    }
}

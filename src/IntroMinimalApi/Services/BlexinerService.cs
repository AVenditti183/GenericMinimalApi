using IntroMinimalApi.Interfaces;
using IntroMinimalApi.Models;

namespace IntroMinimalApi.Services
{
    public class BlexinerService : IEntityService<Blexiner>
    {
        private List<Blexiner> _blexiners = new List<Blexiner>
        {
          new Blexiner { FirstName = "Michele", LastName = "Aponte", JobTitle = "CEO" },
          new Blexiner { FirstName = "Antonio", LastName = "Liccardi", JobTitle = "CTO" },
          new Blexiner { FirstName = "Antonio", LastName = "Venditti", JobTitle = "Architect" },
          new Blexiner { FirstName = "Francesco", LastName = "De Vicariis", JobTitle = "Architect" },
          new Blexiner { FirstName = "Marco", LastName = "Savarese", JobTitle = "Architect" },
          new Blexiner { FirstName = "Adolfo", LastName = "Arnold", JobTitle = "Developer" },
          new Blexiner { FirstName = "Enrico", LastName = "Bencivenga", JobTitle = "Developer" },
          new Blexiner { FirstName = "Gaetano", LastName = "Paternò", JobTitle = "Architect" },
          new Blexiner { FirstName = "Genny", LastName = "Paudice", JobTitle = "Developer" },
          new Blexiner { FirstName = "Anna Maria", LastName = "Serra", JobTitle = "Developer" },
          new Blexiner { FirstName = "Antonio", LastName = "Tammaro", JobTitle = "Developer" },
          new Blexiner { FirstName = "Francesco", LastName = "Vastarella", JobTitle = "Developer" },
          new Blexiner { FirstName = "Gerardo", LastName = "Greco", JobTitle = "Developer" }
        };

        public IEnumerable<Blexiner> GetList(string searchText)
        {
            return _blexiners.Where(b => b.FirstName.Contains(searchText) || b.LastName.Contains(searchText)).ToArray();
        }

        public Blexiner Get(Guid id)
        {
            return _blexiners.SingleOrDefault(b => b.Id == id);
        }

        public Guid Add(Blexiner blexiner)
        {
            _blexiners.Add(blexiner);
            return blexiner.Id;
        }

        public void Update(Guid id, Blexiner updatedBlexiner)
        {
            var blexiner = _blexiners.SingleOrDefault(b => b.Id == id);           
            if(blexiner != null) blexiner = updatedBlexiner;
        }

        public void Delete(Guid id)
        {
            var blexiner = _blexiners.SingleOrDefault(b => b.Id == id);           
            if (blexiner != null) _blexiners.Remove(blexiner);
        }
    }
}

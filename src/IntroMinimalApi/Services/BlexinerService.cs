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

        public IEnumerable<Blexiner> GetList(string searchText)
        {
            var blexiners = _blexiners;
            if (!string.IsNullOrWhiteSpace(searchText)) 
            {
                blexiners = blexiners.Where( b => 
                    b.FirstName.ToLower().Contains(searchText.ToLower()) || 
                    b.LastName.ToLower().Contains(searchText.ToLower())
                ).ToList();
            }
            return blexiners;
        }

        public Blexiner Get(Guid id)
        {
            var blexiner = _blexiners.SingleOrDefault(b => b.Id == id);
            if(blexiner == null)
            {
                throw new KeyNotFoundException();
            }
            return blexiner;
        }

        public Guid Add(Blexiner blexiner)
        {
            _blexiners.Add(blexiner);
            return blexiner.Id;
        }

        public void Update(Guid id, Blexiner updatedBlexiner)
        {
            var blexiner = _blexiners.SingleOrDefault(b => b.Id == id);
            if (blexiner == null)
            {
                throw new KeyNotFoundException();
            }
            blexiner.FirstName = updatedBlexiner.FirstName;
            blexiner.LastName = updatedBlexiner.LastName;
            blexiner.JobTitle = updatedBlexiner.JobTitle;
        }

        public void Delete(Guid id)
        {
            var blexiner = _blexiners.SingleOrDefault(b => b.Id == id);
            if (blexiner == null)
            {
                throw new KeyNotFoundException();
            }
            _blexiners.Remove(blexiner);
        }
    }
}

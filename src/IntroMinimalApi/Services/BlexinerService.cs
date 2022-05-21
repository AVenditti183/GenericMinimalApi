using IntroMinimalApi.Models;
using Entities = IntroMinimalApi.Data.Entities;

namespace IntroMinimalApi.Services
{
    public class BlexinerService : ICrudService<Blexiner>
    {
        private readonly DataStorage _dataStorage;

        public BlexinerService(DataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        
        public IEnumerable<Blexiner> GetList(string searchText)
        {
            IEnumerable<Entities.Blexiner> dsBlexiners = _dataStorage.Blexiners;
            if (!string.IsNullOrWhiteSpace(searchText)) 
            {
                dsBlexiners = dsBlexiners.Where(b =>
                   b.FirstName.ToLower().Contains(searchText.ToLower()) ||
                   b.LastName.ToLower().Contains(searchText.ToLower())
                );
            }
            return dsBlexiners.Select(b => b.ToModel());
        }

        public Blexiner Get(Guid id)
        {
            var dsBlexiner = _dataStorage.Blexiners.SingleOrDefault(b => b.Id == id);
            if(dsBlexiner == null)
            {
                throw new KeyNotFoundException();
            }
            return dsBlexiner.ToModel();
        }

        public Blexiner Add(Blexiner blexiner)
        {
            var dsBlexiner = blexiner.ToEntity();
            _dataStorage.Blexiners.Add(dsBlexiner);
            return dsBlexiner.ToModel();
        }

        public void Update(Guid id, Blexiner blexiner)
        {
            var dsBlexiner = _dataStorage.Blexiners.SingleOrDefault(b => b.Id == id);
            if (dsBlexiner == null)
            {
                throw new KeyNotFoundException();
            }
            dsBlexiner.FirstName = blexiner.FirstName;
            dsBlexiner.LastName = blexiner.LastName;
            dsBlexiner.JobTitle = blexiner.JobTitle;
        }

        public void Delete(Guid id)
        {
            var dsBlexiner = _dataStorage.Blexiners.SingleOrDefault(b => b.Id == id);
            if (dsBlexiner == null)
            {
                throw new KeyNotFoundException();
            }
            _dataStorage.Blexiners.Remove(dsBlexiner);
        }
    }
}

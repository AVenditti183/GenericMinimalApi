using IntroMinimalApi.Models;
using Entities = IntroMinimalApi.Data.Entities;

namespace IntroMinimalApi.Services
{
    public class BlexinerService : IService<Blexiner>
    {
        private readonly DataStorage _dataStorage;

        public BlexinerService(DataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        
        public IEnumerable<Blexiner> GetList(GetListParameters parameters)
        {
            IEnumerable<Entities.Blexiner> dsBlexiners = _dataStorage.Blexiners;
            if (!string.IsNullOrWhiteSpace(parameters.SearchText)) 
            {
                dsBlexiners = dsBlexiners.Where(b =>
                   b.FirstName.ToLower().Contains(parameters.SearchText.ToLower()) ||
                   b.LastName.ToLower().Contains(parameters.SearchText.ToLower())
                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.OrderBy))
            {
                switch (parameters.OrderBy)
                {
                    case "firstname-asc":
                        dsBlexiners = dsBlexiners.OrderBy(c => c.FirstName);
                        break;
                    case "firstname-desc":
                        dsBlexiners = dsBlexiners.OrderByDescending(c => c.FirstName);
                        break;
                    case "lastname-asc":
                        dsBlexiners = dsBlexiners.OrderBy(c => c.LastName);
                        break;
                    case "lastname-desc":
                        dsBlexiners = dsBlexiners.OrderByDescending(c => c.LastName);
                        break;
                }
            }

            if (parameters.PageSize > 0)
            {
                dsBlexiners = dsBlexiners.Skip((parameters.PageNumber - 1) * parameters.PageSize).Take(parameters.PageSize);
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

        public byte[] GetPhoto(Guid id)
        {
            var dsBlexiner = _dataStorage.Blexiners.SingleOrDefault(b => b.Id == id);
            if (dsBlexiner?.Photo == null)
            {
                throw new FileNotFoundException();
            }
            return dsBlexiner.Photo;
        }

        public void AddPhoto(Guid id, FormFileContent photo)
        {
            var dsBlexiner = _dataStorage.Blexiners.SingleOrDefault(b => b.Id == id);
            if (dsBlexiner == null)
            {
                throw new KeyNotFoundException();
            }
            using var stream = photo.Content.OpenReadStream();
            using var photoStream = new MemoryStream();
            stream.CopyTo(photoStream);
            dsBlexiner.Photo = photoStream.ToArray();
        }

        public void DeletePhoto(Guid id)
        {
            var dsBlexiner = _dataStorage.Blexiners.SingleOrDefault(b => b.Id == id);
            if (dsBlexiner?.Photo == null)
            {
                throw new FileNotFoundException();
            }
            dsBlexiner.Photo = null;
        }
    }
}

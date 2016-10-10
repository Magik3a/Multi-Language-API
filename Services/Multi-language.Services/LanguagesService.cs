using Multi_language.Data;
using Multi_language.Models;
using System.Linq;

namespace Multi_language.Services
{
    public class LanguagesService : ILanguagesService
    {
        private readonly IRepository<Languages> languages;

        public LanguagesService(IRepository<Languages> languages)
        {
            this.languages = languages;
        }

        public IQueryable<Languages> GetById(int IdLanguage)
        {
            return languages.All().Where(s => s.IdLanguage == IdLanguage);
        }

        public IQueryable<Languages> GetAll()
        {
            return languages.All();
        }

        public void Add(Languages Language)
        {
            languages.Add(Language);
            Save();
        }

        public void Update(Languages Language)
        {
            languages.Update(Language);
            Save();
        }

        public void Delete(int id)
        {
            var language = languages.GetById(id);
            languages.Delete(language);
            Save();
        }

        private void Save()
        {
            languages.SaveChanges();
        }
    }
}

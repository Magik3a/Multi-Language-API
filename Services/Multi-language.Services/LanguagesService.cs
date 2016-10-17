using Multi_language.Data;
using Multi_language.Models;
using System.Linq;
using System;

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

        public IQueryable<Languages> GetByActiveProject(int ProjectId)
        {
            return languages.All().Where(l => l.IdProject == ProjectId);
        }

        public IQueryable<Languages> GetActiveByActiveProject(int ProjectId)
        {
            return languages.All().Where(l => l.IdProject == ProjectId && l.IsActive == true);

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

        public IQueryable<Languages> GetByInitials(int IdProject, string Initials)
        {
            return languages.All().Where(l => l.IdProject == IdProject && l.Initials == Initials);

        }
    }
}

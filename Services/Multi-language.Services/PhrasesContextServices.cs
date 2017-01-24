using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multi_language.Models;
using Multi_language.Data;

namespace Multi_language.Services
{
    public class PhrasesContextServices : IPhrasesContextServices
    {
        private readonly IRepository<PhrasesContext> phrasesContext;
        private ILanguagesService langService;
        private IPhrasesService phrsService;

        public PhrasesContextServices(IRepository<PhrasesContext> phrasesContext,
            ILanguagesService langService,
            IPhrasesService phrsService)
        {
            this.phrasesContext = phrasesContext;
            this.langService = langService;
            this.phrsService = phrsService;
        }

        public void Add(PhrasesContext PhraseContext)
        {
            phrasesContext.Add(PhraseContext);
            phrasesContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var context = phrasesContext.GetById(id);
            phrasesContext.Delete(context);
            phrasesContext.SaveChanges();
        }

        public IQueryable<PhrasesContext> GetAll()
        {
            return phrasesContext.All() ;
        }

        public PhrasesContext GetById(int IdPhrasesContext)
        {
            return phrasesContext.GetById(IdPhrasesContext);
        }

        public IQueryable<PhrasesContext> GetAllByIdProject(int IdProject, string UserId)
        {
            return phrasesContext.All().Where(pc => pc.IdProject == IdProject);
        }


        public IQueryable<PhrasesContext> GetTranslatedByIdProject(int IdProject, string UserId)
        {
            var activeLang = langService.GetActiveByActiveProject(IdProject).Count();

            return phrasesContext.All().Where(pc => pc.IdProject == IdProject && pc.UserId == UserId && pc.Phrases.Count() == activeLang);

        }
        public IQueryable<PhrasesContext> GetUnTranslatedByIdProject(int IdProject, string UserId)
        {
            var activeLang = langService.GetActiveByActiveProject(IdProject).Count();

            return phrasesContext.All().Where(pc => pc.IdProject == IdProject && pc.UserId == UserId && pc.Phrases.Count() < activeLang);

        }
        public void Update(PhrasesContext PhraseContext)
        {
            phrasesContext.Update(PhraseContext);
            phrasesContext.SaveChanges();
        }
    }
}

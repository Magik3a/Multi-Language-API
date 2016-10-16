using Multi_language.Models;
using System.Linq;

namespace Multi_language.Services
{
    public interface IPhrasesContextServices
    {
        PhrasesContext GetById(int IdLanguage);

        IQueryable<PhrasesContext> GetAll();

        IQueryable<PhrasesContext> GetAllByIdProject(int IdProject, string UserId);

        IQueryable<PhrasesContext> GetTranslatedByIdProject(int IdProject, string UserId);

        void Add(PhrasesContext PhraseContext);

        void Update(PhrasesContext PhraseContext);

        void Delete(int id);

    }
}
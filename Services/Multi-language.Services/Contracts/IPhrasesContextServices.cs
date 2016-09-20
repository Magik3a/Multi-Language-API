using Multi_language.Models;
using System.Linq;

namespace Multi_language.Services
{
    public interface IPhrasesContextServices
    {
        IQueryable<PhrasesContext> GetById(int IdLanguage);

        IQueryable<PhrasesContext> GetAll();

        void Add(PhrasesContext PhraseContext);

        void Update(PhrasesContext PhraseContext);

        void Delete(int id);

        void Save();
    }
}
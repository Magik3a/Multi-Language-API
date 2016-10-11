using Multi_language.Models;
using System.Linq;

namespace Multi_language.Services
{
    public interface IPhrasesService
    {
        Phrases GetById(int IdPhrase);

        IQueryable<Phrases> GetByIdContextAndIdLanguage(int IdPhraseContext, int IdLanguage);

        IQueryable<Phrases> GetAll();

        IQueryable<Phrases> GetAllByIdLanguage(int IdLanguage);

        void Add(Phrases Phrase);

        void Update(Phrases Phrase);

        void Delete(int id);
        
    }
}
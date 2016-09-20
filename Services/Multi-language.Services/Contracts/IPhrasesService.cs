using Multi_language.Models;
using System.Linq;

namespace Multi_language.Services
{
    public interface IPhrasesService
    {
        IQueryable<Phrases> GetById(int IdPhrase);

        IQueryable<Phrases> GetAll();

        void Add(Phrases Phrase);

        void Update(Phrases Phrase);

        void Delete(int id);

        void Save();
    }
}
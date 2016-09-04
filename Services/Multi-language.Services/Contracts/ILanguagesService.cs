using Multi_language.Models;
using System.Linq;

namespace Multi_language.Services
{
    public interface ILanguagesService
    {
        IQueryable<Languages> GetById(int IdLanguage);

        IQueryable<Languages> GetAll();

        void Add(Languages Language);

        void Update(Languages Language);

        void Delete(Languages Language);
    }
}
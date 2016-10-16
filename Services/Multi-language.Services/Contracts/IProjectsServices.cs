using Multi_language.Models;
using System.Linq;

namespace Multi_language.Services
{
    public interface IProjectsServices
    {
        Projects GetById(int IdProject);

        IQueryable<Projects> GetAll();

        IQueryable<Projects> GetForUser(string UserId);


        void Add(Projects Project);

        void Update(Projects Project);

        void Delete(int id);
    }
}
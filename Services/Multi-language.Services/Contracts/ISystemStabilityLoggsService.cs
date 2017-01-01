using System.Linq;
using Multi_language.Models;

namespace Multi_language.Services
{
    public interface ISystemStabilityLoggsService
    {
        void Add(SystemStabilityLogg systemStabilityLogg);

        IQueryable<SystemStabilityLogg> GetAll();

        IQueryable<SystemStabilityLogg> GetAllBeforeHours(int hours);
    }
}
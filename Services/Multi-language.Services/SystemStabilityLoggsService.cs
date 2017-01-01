using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multi_language.Data;
using Multi_language.Models;

namespace Multi_language.Services
{
    public class SystemStabilityLoggsService : ISystemStabilityLoggsService
    {
        private readonly IRepository<SystemStabilityLogg> systemStabilityLoggRepo;

        public SystemStabilityLoggsService(IRepository<SystemStabilityLogg> systemStabilityLogg)
        {
            this.systemStabilityLoggRepo = systemStabilityLogg;
        }
        public void Add(SystemStabilityLogg systemStabilityLogg)
        {
            systemStabilityLoggRepo.Add(systemStabilityLogg);
            systemStabilityLoggRepo.SaveChanges();
        }

        public IQueryable<SystemStabilityLogg> GetAll()
        {
            return systemStabilityLoggRepo.All();
        }

        public IQueryable<SystemStabilityLogg> GetAllBeforeHours(int hours)
        {
            var before = DateTime.Now.AddHours(hours);
            return systemStabilityLoggRepo.All().Where(s => s.DateCreated > before);
        }
    }
}

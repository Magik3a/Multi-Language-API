using Multi_language.Data;
using Multi_language.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multi_language.Services
{
    public class ProjectsServices : IProjectsServices
    {
        private readonly IRepository<Projects> projects;
        public ProjectsServices(IRepository<Projects> projects)
        {
            this.projects = projects;
        }

        public void Add(Projects Project)
        {
            projects.Add(Project);
            projects.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = projects.GetById(id);
            projects.Delete(project);
            projects.SaveChanges();
        }

        public IQueryable<Projects> GetAll()
        {
            return projects.All();
        }

        public Projects GetById(int IdProject)
        {
            return projects.GetById(IdProject);
        }

        public void Update(Projects Project)
        {
            projects.Update(Project);
            projects.SaveChanges();
        }
    }
}

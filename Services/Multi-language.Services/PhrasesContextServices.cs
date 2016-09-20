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

        public PhrasesContextServices(IRepository<PhrasesContext> phrasesContext)
        {
            this.phrasesContext = phrasesContext;
        }

        public void Add(PhrasesContext PhraseContext)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<PhrasesContext> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<PhrasesContext> GetById(int IdLanguage)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(PhrasesContext PhraseContext)
        {
            throw new NotImplementedException();
        }
    }
}

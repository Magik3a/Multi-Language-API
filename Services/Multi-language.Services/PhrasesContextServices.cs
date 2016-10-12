﻿using System;
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
            phrasesContext.Add(PhraseContext);
            phrasesContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var context = phrasesContext.GetById(id);
            phrasesContext.Delete(context);
            phrasesContext.SaveChanges();
        }

        public IQueryable<PhrasesContext> GetAll()
        {
            return phrasesContext.All() ;
        }

        public PhrasesContext GetById(int IdPhrasesContext)
        {
            return phrasesContext.GetById(IdPhrasesContext);
        }


        public void Update(PhrasesContext PhraseContext)
        {
            phrasesContext.Update(PhraseContext);
            phrasesContext.SaveChanges();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Multi_language.Models;
using Multi_language.Data;

namespace Multi_language.Services
{
    public class PhrasesService : IPhrasesService
    {
        private readonly IRepository<Phrases> phrases;
        private readonly IRepository<PhrasesContext> phrasesContext;

        public PhrasesService(IRepository<Phrases> phrases, IRepository<PhrasesContext> phrasesContext)
        {
            this.phrases = phrases;
            this.phrasesContext = phrasesContext;
        }

        public void Add(Phrases Phrase)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Phrases> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Phrases> GetById(int IdPhrase)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Phrases Phrase)
        {
            throw new NotImplementedException();
        }
    }
}

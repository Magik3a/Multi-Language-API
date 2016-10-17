﻿using Multi_language.Models;
using System.Linq;

namespace Multi_language.Services
{
    public interface IPhrasesService
    {
        Phrases GetById(int IdPhrase);

        string GetTextPhrase(int IdPhraseContext, int IdLanguage);

        IQueryable<Phrases> GetAll();

        IQueryable<Phrases> GetAllByIdProjectAndIdContext(int IdProject, int IdContext);

        IQueryable<Phrases> GetAllByIdLanguage(int IdLanguage);

        IQueryable<Phrases> GetAllByIdProject(int IdProject, string UserId);

        void Add(Phrases Phrase);

        void Update(Phrases Phrase);

        void Delete(int id);

    }
}
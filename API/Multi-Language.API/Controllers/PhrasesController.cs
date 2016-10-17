using AutoMapper;
using AutoMapper.QueryableExtensions;
using Multi_language.Models;
using Multi_language.Services;
using Multi_Language.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Multi_Language.API.Controllers
{
    public class PhrasesController : ApiController
    {
        private ILanguagesService langService;
        private IPhrasesService phrasesService;
        private IPhrasesContextServices phrasesContextService;

        public PhrasesController(ILanguagesService langService,
            IPhrasesService phrasesService,
            IPhrasesContextServices phrasesContextService)
        {
            this.langService = langService;
            this.phrasesService = phrasesService;
            this.phrasesContextService = phrasesContextService;
        }
        /// <summary>
        /// Get exact phase by language.
        /// </summary>
        [ResponseType(typeof(IEnumerable<PhrasesApiModel>))]
        public IHttpActionResult Get()
        {
            var phrase = phrasesService.GetAll().ProjectTo<PhrasesApiModel>().ToList();

            return Ok(phrase);
        }

        /// <summary>
        /// Get exact phase by language.
        /// </summary>
        [ResponseType(typeof(IEnumerable<PhrasesApiModel>))]
        public IHttpActionResult Get(int idProject)
        {
            var phrase = phrasesService.GetAllByIdLanguage(idProject).ProjectTo<PhrasesApiModel>().ToList();

            return Ok(phrase);
        }

        /// <summary>
        /// Add new phrase
        /// </summary>
        /// <param model="PhrasesApiModel"></param>
        public IHttpActionResult Post(PhrasesApiModel phrase)
        {
            if (ModelState.IsValid && phrasesService.GetAll()
                .Where(p => p.IdPhraseContext == phrase.IdPhraseContext && p.IdLanguage == phrase.IdLanguage ).Count() == 0)
            {
                phrase.DateChanged = DateTime.Now;
                phrase.DateCreated = DateTime.Now;
                phrasesService.Add(Mapper.Map<PhrasesApiModel, Phrases>(phrase));

                return Ok();
            }

            return BadRequest("Phrase with this translation already exist! Delete or edit existing one");
        }
    }
}

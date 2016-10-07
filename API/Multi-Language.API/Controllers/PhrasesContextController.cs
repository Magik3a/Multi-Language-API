using AutoMapper;
using Multi_language.Models;
using Multi_language.Services;
using Multi_Language.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Multi_Language.API.Controllers
{
    public class PhrasesContextController : ApiController
    {
        private ILanguagesService langService;
        private IPhrasesService phrasesService;
        private IPhrasesContextServices phrasesContextService;

        public PhrasesContextController(ILanguagesService langService,
            IPhrasesService phrasesService,
            IPhrasesContextServices phrasesContextService)
        {
            this.langService = langService;
            this.phrasesService = phrasesService;
            this.phrasesContextService = phrasesContextService;
        }

        /// <summary>
        /// Add new phrase context
        /// </summary>
        /// <param model="PhrasesContextApiModel"></param>
        public IHttpActionResult Post(PhrasesContextApiModel phraseContext)
        {
            if (ModelState.IsValid)
            {
                phraseContext.DateChanged = DateTime.Now;
                phraseContext.DateCreated = DateTime.Now;
                phrasesContextService.Add(Mapper.Map<PhrasesContextApiModel, PhrasesContext>(phraseContext));
                phrasesContextService.Save();

                return Ok();
            }

            return BadRequest();
        }

        
    }
}

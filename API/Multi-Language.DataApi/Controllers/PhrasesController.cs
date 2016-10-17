using AutoMapper;
using AutoMapper.QueryableExtensions;
using Multi_language.Models;
using Multi_language.Services;
using Multi_Language.DataApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace Multi_Language.DataApi.Controllers
{
    [RoutePrefix("api/Resources")]

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
        [Route("GetAll")]
        [ResponseType(typeof(IEnumerable<PhrasesApiModel>))]
        public IHttpActionResult Get()
        {
            var phrase = phrasesService.GetAll().ProjectTo<PhrasesApiModel>().ToList();

            return Ok(phrase);
        }

        /// <summary>
        /// Get exact phase by id and language.
        /// </summary>
        [ResponseType(typeof(PhrasesApiModel))]
        public IHttpActionResult Get(int id, string language)
        {
            var idLanguage = langService.GetAll().Where(l => l.Culture == language).FirstOrDefault().IdLanguage;
            var phrase = phrasesService.GetByIdContextAndIdLanguage(id, idLanguage).ProjectTo<PhrasesApiModel>().FirstOrDefault();

            return Ok(phrase);
        }


    }
}

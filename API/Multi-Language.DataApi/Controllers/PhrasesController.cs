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
using WebApi.OutputCache.V2;

namespace Multi_Language.DataApi.Controllers
{
    [RoutePrefix("")]

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

        public class Header
        {
            public string sender { get; set; }
        }

        public class Body
        {
            public string @class { get; set; }
            public string type { get; set; }
        }

        public class RootObject
        {
            public Header header { get; set; }
            public Body body { get; set; }
        }

        /// <summary>
        /// Get exact phase by language.
        /// </summary>
        [Route("Project/{idProject}")]
        [ResponseType(typeof(IEnumerable<PhrasesApiModel>))]
        [CacheOutput(ClientTimeSpan = 1200, ServerTimeSpan = 1200)]
        public IHttpActionResult GetForProject(int idProject)
        {
            var phrase = phrasesService.GetAllByIdProject(idProject, "").ProjectTo<PhrasesApiModel>().ToList();

            return Ok(phrase);
        }

        /// <summary>
        /// Get exact phase by language.
        /// </summary>
        [Route("Project/{idProject}/Initials/{initials}")]
        [ResponseType(typeof(IEnumerable<PhrasesApiModel>))]
        [CacheOutput(ClientTimeSpan = 1200, ServerTimeSpan = 1200)]
        public IHttpActionResult GetForLanguage(int idProject, string initials)
        {
            var language = langService.GetByInitials(idProject, initials).FirstOrDefault();
            if (language != null)
            {
                return Ok(phrasesService.GetAllByIdLanguage(language.IdLanguage).ProjectTo<PhrasesApiModel>().ToList());

            }
            return NotFound();

        }


        /// <summary>
        /// Get exact phase by language.
        /// </summary>
        [Route("Project/{idProject}/Context/{idContext}")]
        [ResponseType(typeof(IEnumerable<PhrasesApiModel>))]
        [CacheOutput(ClientTimeSpan = 1200, ServerTimeSpan = 1200)]
        public IHttpActionResult GetForContext(int idProject, int idContext)
        {
            return Ok(phrasesService.GetAllByIdProjectAndIdContext(idProject, idContext).ProjectTo<PhrasesApiModel>().ToList());

        }

        /// <summary>
        /// Get exact phase by id and language.
        /// </summary>
        [Route("Project/{idProject}/Initials/{initials}/Phrase/{idPhrase}")]
        [ResponseType(typeof(string))]
        [CacheOutput(ClientTimeSpan = 1200, ServerTimeSpan = 1200)]
        public IHttpActionResult GetPhrase(int idProject, string initials, int idPhrase)
        {
            var language = langService.GetByInitials(idProject, initials).FirstOrDefault();
            if (language != null)
            {
                return Ok(phrasesService.GetTextPhrase(idPhrase, language.IdLanguage));

            }

            return NotFound();
        }


    }
}

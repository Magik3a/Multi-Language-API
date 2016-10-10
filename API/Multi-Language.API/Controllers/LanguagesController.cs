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
    public class LanguagesController : ApiController
    {
        private ILanguagesService langService;
        public LanguagesController(ILanguagesService langService)
        {
            this.langService = langService;
        }


        /// <summary>
        /// Get List with all the languages. 
        /// </summary>
        [ResponseType(typeof(IEnumerable<LanguagesApiModel>))]
        public IHttpActionResult Get()
        {
            var lstLanguages = langService.GetAll().ProjectTo<LanguagesApiModel>().ToList();

            return Ok(lstLanguages);
        }

        /// <summary>
        /// Get exact language by id. 
        /// </summary>
        [ResponseType(typeof(LanguagesApiModel))]
        public IHttpActionResult Get(int id)
        {
            var language = langService.GetById(id).ProjectTo<LanguagesApiModel>().FirstOrDefault();
    
            return Ok(language);
        }

        /// <summary>
        /// Add new language
        /// </summary>
        /// <param model="LanguagesApiModel"></param>
        public IHttpActionResult Post(LanguagesApiModel language)
        {
            if (ModelState.IsValid)
            {
                language.Datechanged = DateTime.Now;
                language.DateCreated = DateTime.Now;
                langService.Add(Mapper.Map<LanguagesApiModel, Languages>(language));

                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// Update language 
        /// </summary>
        /// <param LanguagesApiModel="language"></param>
        /// <returns>If succeeded Response Ok</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult Put(int id, LanguagesApiModel language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            language.IdLanguage = id;
            language.Datechanged = DateTime.Now;
            langService.Update(Mapper.Map<LanguagesApiModel, Languages>(language));

            return Ok();
        }

        // DELETE: api/Languages/5
        public IHttpActionResult Delete(int id)
        {
            if (langService.GetById(id).FirstOrDefault() != null)
                langService.Delete(id);
            else
                return BadRequest();
            
            return Ok();
        }
    }
}

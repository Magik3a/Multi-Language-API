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
        public IEnumerable<LanguagesApiModel> Get()
        {
            var lstLanguages2 = langService.GetAll().ToList();

            var lstLanguages = langService.GetAll().ProjectTo<LanguagesApiModel>().ToList();

            return lstLanguages;
        }

        // GET: api/Languages/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Languages
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Languages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Languages/5
        public void Delete(int id)
        {
        }
    }
}

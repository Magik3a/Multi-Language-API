using Multi_language.Models;
using Multi_language.Services;
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
        // GET: api/Languages
        public IEnumerable<Languages> Get()
        {
            var lstLanguages = langService.GetAll();

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

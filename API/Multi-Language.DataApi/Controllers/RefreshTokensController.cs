using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.SignalR;
using Multi_Language.DataApi.Hubs;

namespace Multi_Language.DataApi.Controllers
{
    [RoutePrefix("api/RefreshTokens")]
    public class RefreshTokensController : ApiController
    {

        private AuthRepository _repo = null;

        public RefreshTokensController()
        {
            _repo = new AuthRepository();
        }

        [System.Web.Http.Authorize(Users = "Admin")]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_repo.GetAllRefreshTokens());
        }

        //[Authorize(Users = "Admin")]
        [AllowAnonymous]
        [Route("")]
        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await _repo.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }

        [Route("Expires")]
        public IHttpActionResult Expires(string tokenId)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<InternalHub>();
            context.Clients.All.tokenIsExpired(true);
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
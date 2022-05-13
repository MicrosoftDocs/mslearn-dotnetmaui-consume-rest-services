using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartsService.Models;

namespace PartsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : BaseController
    {
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var authorizationToken = Guid.NewGuid().ToString();

                PartsFactory.Initialize(authorizationToken);

                return new JsonResult(authorizationToken);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}

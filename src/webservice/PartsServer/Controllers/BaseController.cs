using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartsService.Models;
using System.Net;

namespace PartsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected List<Part> UserParts
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.AuthorizationToken))
                {
                    return null;
                }

                if (!PartsFactory.Parts.ContainsKey(this.AuthorizationToken))
                {
                    return null;
                }

                var result = PartsFactory.Parts[this.AuthorizationToken];

                return result.Item2;
            }
        }

        protected bool CheckAuthorization()
        {
            PartsFactory.ClearStaleData();

            try
            {
                var ctx = HttpContext;
                if (ctx != null)
                {
                    if (string.IsNullOrWhiteSpace(this.AuthorizationToken))
                    {
                        ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        return false;
                    }
                }
                else
                {
                    return false;
                }

                if (!PartsFactory.Parts.ContainsKey(this.AuthorizationToken))
                {
                    return false;
                }

                return true;
            }
            catch
            {
            }

            return false;
        }

        protected string AuthorizationToken
        {
            get
            {
                string authorizationToken = string.Empty;

                var ctx = HttpContext;
                if (ctx != null)
                {
                    authorizationToken = ctx.Request.Headers["Authorization"].ToString();
                }

                return authorizationToken;
            }
        }
    }
}

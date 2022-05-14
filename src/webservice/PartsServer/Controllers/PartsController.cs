using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PartsService.Models;
using System.Net;
using System.Text.Json;

namespace PartsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : BaseController
    {
        [HttpGet]
        public ActionResult Get()
        {
            var authorized = CheckAuthorization();
            if (!authorized)
            {
                return Unauthorized();
            }
            Console.WriteLine("GET /api/parts");
            return new JsonResult(UserParts);
        }

        [HttpGet("{partid}")]
        public ActionResult Get(string partid)
        {
            var authorized = CheckAuthorization();
            if (!authorized)
            {
                return Unauthorized();
            }

            if (string.IsNullOrEmpty(partid))
                return this.BadRequest();

            partid = partid.ToUpperInvariant();
            Console.WriteLine($"GET /api/parts/{partid}");
            var userParts = UserParts;
            var part = userParts.SingleOrDefault(x => x.PartID == partid);

            if (part == null)
            {
                return this.NotFound();
            }
            else
            {
                return this.Ok(part);
            }
        }

        [HttpPut("{partid}")]
        public HttpResponseMessage Put(string partid, [FromBody] Part part)
        {
            try
            {
                var authorized = CheckAuthorization();
                if (!authorized)
                {
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }

                if (!ModelState.IsValid)
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }

                if (string.IsNullOrEmpty(part.PartID))
                {
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
                }

                Console.WriteLine($"PUT /api/parts/{partid}");
                Console.WriteLine(JsonSerializer.Serialize(part));


                var userParts = UserParts;
                var existingParts = userParts.SingleOrDefault(x => x.PartID == partid);
                if (existingParts != null)
                {
                    existingParts.Suppliers = part.Suppliers;
                    existingParts.PartType = part.PartType;
                    existingParts.PartAvailableDate = part.PartAvailableDate;
                    existingParts.PartName = part.PartName;
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Part part)
        {
            try
            {
                var authorized = CheckAuthorization();
                if (!authorized)
                {
                    return this.Unauthorized();
                }

                if (!string.IsNullOrWhiteSpace(part.PartID))
                {
                    return this.BadRequest();
                }
                Console.WriteLine($"POST /api/parts");
                Console.WriteLine(JsonSerializer.Serialize(part));

                part.PartID = PartsFactory.CreatePartID();

                if (!ModelState.IsValid)
                {
                    return this.BadRequest();
                }

                var userParts = UserParts;

                if (userParts.Any(x => x.PartID == part.PartID))
                {
                    return this.Conflict();
                }

                userParts.Add(part);

                return this.Ok(part);
            }
            catch (Exception ex)
            {
                return this.Problem("Internal server error");
            }
        }

        [HttpDelete]
        [Route("{partid}")]
        public HttpResponseMessage Delete(string partid)
        {
            try
            {
                var authorized = CheckAuthorization();
                if (!authorized)
                {
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }

                var userParts = UserParts;
                var existingParts = userParts.SingleOrDefault(x => x.PartID == partid);

                if (existingParts == null)
                {
                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                }
                Console.WriteLine($"POST /api/parts/{partid}");
                userParts.RemoveAll(x => x.PartID == partid);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}

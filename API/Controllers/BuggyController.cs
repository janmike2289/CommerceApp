using API.DTO;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseAPIController
    {
        [HttpGet("unauthorized")]
        public IActionResult GetUnauthorized()
        {
            return Unauthorized("You are not authorized");
        }

        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {
            return Unauthorized("Not a good request");
        }

        [HttpGet("notfound")]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("internalerror")]
        public IActionResult GetInternalError()
        {
            throw new Exception("This is a test exception");
        }

        [HttpGet("validationerror")]
        public IActionResult GetValidationError(CreateProductDTO product)
        {
            return Ok();
        }
    }
}

using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ValidVacations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.All(e => e.Type == ErrorType.Validation))
            {
                var modetStateDictionary = new ModelStateDictionary();
                
                foreach (var error in errors)
                    modetStateDictionary.AddModelError(error.Code, error.Description);

                return ValidationProblem(modetStateDictionary);
            }

            if (errors.Any(e => e.Type == ErrorType.Unexpected))
            {
                return Problem();
            }

            var firstError = errors[0];

            var statusCode = GetStatusCode(firstError.Type);

            return Problem(statusCode.ToString(), firstError.Description);
        }

        private int GetStatusCode(ErrorType errorType)
        {
            switch (errorType)
            {
                case ErrorType.Failure:
                    return StatusCodes.Status417ExpectationFailed;
                case ErrorType.Validation:
                    return StatusCodes.Status400BadRequest;
                case ErrorType.Conflict:
                    return StatusCodes.Status409Conflict;
                case ErrorType.NotFound:
                    return StatusCodes.Status404NotFound;
                case ErrorType.Unauthorized:
                    return StatusCodes.Status401Unauthorized;
                case ErrorType.Forbidden:
                    return StatusCodes.Status403Forbidden;
                default:
                    return StatusCodes.Status500InternalServerError;
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext)
        {
            var Errors = actionContext.ModelState.Where(predicate: E => E.Value.Errors.Count > 0)
                .ToDictionary(keySelector: X => X.Key, elementSelector: X => X.Value.Errors.Select(selector: X => X.ErrorMessage).ToArray());
            var Problem = new ProblemDetails()
            {
                Title = "Validation Errors",
                Detail = "One or more validation errors occurred",
                Status = StatusCodes.Status400BadRequest,
                Extensions = { { "Errors", Errors } }
            };
            return new BadRequestObjectResult(error: Problem);
        }
    }
}

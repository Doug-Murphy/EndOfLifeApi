using DougMurphy.TargetFrameworks.EndOfLife.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EndOfLifeApi.Filters;

public class HttpResponseExceptionFilterAttribute : ExceptionFilterAttribute {
	public override void OnException(ExceptionContext context) {
		switch (context.Exception) {
			case ArgumentException:
			case TargetFrameworkUnknownException:
				context.Result = new BadRequestObjectResult(new ProblemDetails {Detail = context.Exception.Message});
				context.ExceptionHandled = true;
				break;
		}
	}
}

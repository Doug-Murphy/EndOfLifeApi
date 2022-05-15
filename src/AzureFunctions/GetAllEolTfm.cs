using DougMurphy.TargetFrameworks.EndOfLife.Enums;
using DougMurphy.TargetFrameworks.EndOfLife.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace EndOfLifeApi.AzureFunctions;

public static class GetAllEolTfm {
	[FunctionName("GetAllEolTfm")]
	public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-all-eol")] HttpRequest req,
	                                                 ILogger log) {
		log.LogInformation("Getting all EOL TFMs with query params {QueryParams}", req.QueryString);

		if (Enum.TryParse(req.Query["timeframeUnit"].ToString(), out TimeframeUnit timeframeUnit) && byte.TryParse(req.Query["timeframeAmount"], out byte timeframeAmount)) {
			return new OkObjectResult(TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(timeframeUnit, timeframeAmount));
		}

		return new OkObjectResult(TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers());
	}
}

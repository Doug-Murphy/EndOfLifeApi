using DougMurphy.TargetFrameworks.EndOfLife.Helpers;
using DougMurphy.TargetFrameworks.EndOfLife.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace EndOfLifeApi.AzureFunctions;

public static class CheckTfmEol {
	[FunctionName("CheckTfmEol")]
	public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "check-eol/{tfm:regex([a-zA-Z0-9.]{{3,}}):required}")] HttpRequest req,
	                                                 string tfm,
	                                                 ILogger log) {
		log.LogInformation("Checking {TargetFrameworkMoniker} for EOL", tfm);

		ArgumentNullException.ThrowIfNull(tfm);

		TargetFrameworkCheckResponse endOfLifeResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfm);
		if (endOfLifeResults.EndOfLifeTargetFrameworks.Count == 0) {
			return new NoContentResult();
		}

		return new OkObjectResult(endOfLifeResults);
	}
}

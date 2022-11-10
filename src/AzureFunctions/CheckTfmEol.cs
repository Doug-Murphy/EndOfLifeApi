using DougMurphy.TargetFrameworks.EndOfLife.Helpers;
using DougMurphy.TargetFrameworks.EndOfLife.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EndOfLifeApi.AzureFunctions;

public class CheckTfmEol {
	private readonly ILogger<CheckTfmEol> _logger;

	public CheckTfmEol(ILogger<CheckTfmEol> logger) {
		_logger = logger;
	}

	[Function("CheckTfmEol")]
	public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "check-eol/{tfm:regex([a-zA-Z0-9.]{{3,}}):required}")] HttpRequestData req, string tfm) {
		_logger.LogInformation("Checking {TargetFrameworkMoniker} for EOL", tfm);

		ArgumentNullException.ThrowIfNull(tfm);

		TargetFrameworkCheckResponse endOfLifeResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfm);
		if (endOfLifeResults.EndOfLifeTargetFrameworks.Count == 0) {
			return req.CreateResponse(HttpStatusCode.NoContent);
		}

		HttpResponseData response = req.CreateResponse();
		await response.WriteAsJsonAsync(endOfLifeResults, HttpStatusCode.OK);
		return response;
	}
}

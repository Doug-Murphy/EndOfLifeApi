using DougMurphy.TargetFrameworks.EndOfLife.Enums;
using DougMurphy.TargetFrameworks.EndOfLife.Helpers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace EndOfLifeApi.AzureFunctions;

public class GetAllEolTfm {
	private readonly ILogger<GetAllEolTfm> _logger;

	public GetAllEolTfm(ILogger<GetAllEolTfm> logger) {
		_logger = logger;
	}
	
	[Function("GetAllEolTfm")]
	public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "get-all-eol")] HttpRequestData req, string? timeframeUnit, byte? timeframeAmount) {
		_logger.LogInformation("Getting all EOL TFMs with query parameters {TimeframeUnit} and {TimeframeAmount}", timeframeUnit, timeframeAmount);

		HttpResponseData response = req.CreateResponse();

		if (Enum.TryParse(timeframeUnit, out TimeframeUnit timeframeUnitParsed) && timeframeAmount.HasValue) {
			await response.WriteAsJsonAsync(TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(timeframeUnitParsed, timeframeAmount.Value), HttpStatusCode.OK);
			return response;
		}

		await response.WriteAsJsonAsync(TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(), HttpStatusCode.OK);
		return response;
	}
}

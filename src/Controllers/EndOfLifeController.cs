using EndOfLifeApi.Exceptions;
using EndOfLifeApi.Helpers;
using EndOfLifeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EndOfLifeApi.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class EndOfLifeController : ControllerBase {
		/// <summary>Determine whether or not given Target Framework Moniker(s) is/are currently end of life.</summary>
		/// <param name="tfm">The TFM to check. Can be a singular TFM or a semicolon-delimited list of TFMs.</param>
		/// <returns></returns>
		[HttpGet]
		[Route("check-eol/{tfm}")]
		[ProducesResponseType(typeof(TargetFrameworkCheckResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public IActionResult CheckTargetFrameworkForEndOfLife(string tfm) {
			try {
				ArgumentNullException.ThrowIfNull(tfm);

				TargetFrameworkCheckResponse endOfLifeResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfm);
				if (endOfLifeResults.EndOfLifeTargetFrameworks.Length == 0) {
					return NoContent();
				}

				return Ok(endOfLifeResults);
			}
			catch (ArgumentNullException ex) {
				return BadRequest(new ProblemDetails {Detail = ex.Message});
			}
			catch (ArgumentException ex) {
				return BadRequest(new ProblemDetails {Detail = ex.Message});
			}
			catch (TargetFrameworkUnknownException ex) {
				return BadRequest(new ProblemDetails {Detail = ex.Message});
			}
		}
	}
}

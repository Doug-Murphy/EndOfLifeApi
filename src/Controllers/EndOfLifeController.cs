using EndOfLifeApi.Exceptions;
using EndOfLifeApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EndOfLifeApi.Controllers {
	[ApiController]
	[Route("[controller]")]
	public class EndOfLifeController : ControllerBase {
		/// <summary>Determine whether or not a singular Target Framework Moniker is currently end of life.</summary>
		/// <param name="singularTfm">The singular TFM to check.</param>
		/// <returns></returns>
		[HttpGet]
		[Route("single-tfm/{singularTfm}")]
		[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public IActionResult CheckIfSingleTargetFrameworkIsEndOfLife(string singularTfm) {
			try {
				ArgumentNullException.ThrowIfNull(singularTfm);

				return Ok(TargetFrameworkEndOfLifeHelper.IsSingularTfmEol(singularTfm));
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

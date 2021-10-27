using EndOfLifeApi.Filters;
using EndOfLifeApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EndOfLifeApi.Controllers {
	[ApiController]
	[TypeFilter(typeof(HandleExceptionsFilter))]
	[Route("[controller]")]
	public class EndOfLifeController : ControllerBase {
		/// <summary>Determine whether or not a singular Target Framework Moniker is currently end of life.</summary>
		/// <param name="singularTfm">The singular TFM to check.</param>
		/// <returns></returns>
		[HttpGet]
		[Route("single-tfm/{singularTfm}")]
		[ProducesResponseType(typeof(bool), 200)]
		public IActionResult CheckIfSingleTargetFrameworkIsEndOfLife(string singularTfm) {
			ArgumentNullException.ThrowIfNull(singularTfm);

			return Ok(TargetFrameworkEndOfLifeHelper.IsSingularTfmEol(singularTfm));
		}
	}
}

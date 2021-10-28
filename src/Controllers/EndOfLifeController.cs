﻿using EndOfLifeApi.Enums;
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
			ArgumentNullException.ThrowIfNull(tfm);

			TargetFrameworkCheckResponse endOfLifeResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfm);
			if (endOfLifeResults.EndOfLifeTargetFrameworks.Length == 0) {
				return NoContent();
			}

			return Ok(endOfLifeResults);
		}

		/// <summary>Get a list of all Target Framework Monikers that are currently end of life or will be end of life within a specified timeframe.</summary>
		/// <param name="timeframeUnit">The unit of the timeframe. eg. day, week, etc.</param>
		/// <param name="timeframeAmount">How many of the unit to forecast ahead for. eg. 3 weeks, 4 months, etc.</param>
		/// <returns></returns>
		[HttpGet]
		[Route("get-all-eol")]
		[ProducesResponseType(typeof(TargetFrameworkCheckResponse), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
		[ProducesDefaultResponseType]
		public IActionResult GetEndOfLifeTargetFrameworks(TimeframeUnit? timeframeUnit, byte? timeframeAmount) {
			TargetFrameworkCheckResponse endOfLifeResults = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(timeframeUnit, timeframeAmount);

			return Ok(endOfLifeResults);
		}
	}
}

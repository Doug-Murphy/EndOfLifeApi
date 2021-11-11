using EndOfLifeApi.Enums;
using EndOfLifeApi.Exceptions;
using EndOfLifeApi.Models;
using System.Collections.Immutable;

namespace EndOfLifeApi.Helpers {
	public static class TargetFrameworkEndOfLifeHelper {
		//TFM list found on https://docs.microsoft.com/en-us/dotnet/standard/frameworks
		private static readonly ImmutableDictionary<string, DateOnly?> TargetFrameworksWithEndOfLifeDate = new Dictionary<string, DateOnly?> {
			//.NET Standard does not have an EOL
			{"netstandard1.0", null},
			{"netstandard1.1", null},
			{"netstandard1.2", null},
			{"netstandard1.3", null},
			{"netstandard1.4", null},
			{"netstandard1.5", null},
			{"netstandard1.6", null},
			{"netstandard2.0", null},
			{"netstandard2.1", null},

			//EOL for .NET Framework versions found on https://docs.microsoft.com/en-us/lifecycle/products/microsoft-net-framework
			{"net11", new DateOnly(2011, 07, 12)},
			{"v1.1", new DateOnly(2011, 07, 12)},
			{"net20", new DateOnly(2011, 07, 12)},
			{"v2.0", new DateOnly(2011, 07, 12)},
			{"net30", new DateOnly(2011, 07, 12)}, //net30 is not shown on Microsoft's exhaustive list of TFMs, so this TFM is assumed
			{"v3.0", new DateOnly(2011, 07, 12)},
			{"net35", new DateOnly(2029, 01, 09)},
			{"v3.5", new DateOnly(2029, 01, 09)},
			{"net40", new DateOnly(2016, 01, 12)},
			{"v4.0", new DateOnly(2016, 01, 12)},
			{"net403", new DateOnly(2016, 01, 12)}, //assumption to match 4.0 and 4.5
			{"v4.0.3", new DateOnly(2016, 01, 12)}, //assumption to match 4.0 and 4.5
			{"net45", new DateOnly(2016, 01, 12)},
			{"v4.5", new DateOnly(2016, 01, 12)},
			{"net451", new DateOnly(2016, 01, 12)},
			{"v4.5.1", new DateOnly(2016, 01, 12)},
			{"net452", new DateOnly(2022, 04, 26)},
			{"v4.5.2", new DateOnly(2022, 04, 26)},
			{"net46", new DateOnly(2022, 04, 26)},
			{"v4.6", new DateOnly(2022, 04, 26)},
			{"net461", new DateOnly(2022, 04, 26)},
			{"v4.6.1", new DateOnly(2022, 04, 26)},
			{"net462", null},
			{"v4.6.2", null},
			{"net47", null},
			{"v4.7", null},
			{"net471", null},
			{"v4.7.1", null},
			{"net472", null},
			{"v4.7.2", null},
			{"net48", null},
			{"v4.8", null},

			//EOL for .NET Core and .NET 5+ found on https://docs.microsoft.com/en-us/lifecycle/products/microsoft-net-and-net-core
			{"netcoreapp1.0", new DateOnly(2019, 06, 27)},
			{"netcoreapp1.1", new DateOnly(2019, 06, 27)},
			{"netcoreapp2.0", new DateOnly(2018, 10, 01)},
			{"netcoreapp2.1", new DateOnly(2021, 08, 21)},
			{"netcoreapp2.2", new DateOnly(2019, 12, 23)},
			{"netcoreapp3.0", new DateOnly(2020, 03, 03)},
			{"netcoreapp3.1", new DateOnly(2022, 12, 03)},

			//.NET 5 TFMs taken from https://github.com/dotnet/designs/blob/main/accepted/2020/net5/net5.md
			{"net5.0", new DateOnly(2022, 05, 08)},
			{"net5.0-android", new DateOnly(2022, 05, 08)},
			{"net5.0-ios", new DateOnly(2022, 05, 08)},
			{"net5.0-macos", new DateOnly(2022, 05, 08)},
			{"net5.0-tvos", new DateOnly(2022, 05, 08)},
			{"net5.0-watchos", new DateOnly(2022, 05, 08)},
			{"net5.0-windows", new DateOnly(2022, 05, 08)},

			//.NET 6 TFMs taken from https://github.com/dotnet/designs/blob/main/accepted/2021/net6.0-tfms/net6.0-tfms.md
			{"net6.0", new DateOnly(2024, 11, 08)},
			{"net6.0-android", new DateOnly(2024, 11, 08)},
			{"net6.0-ios", new DateOnly(2024, 11, 08)},
			{"net6.0-macos", new DateOnly(2024, 11, 08)},
			{"net6.0-maccatalyst", new DateOnly(2024, 11, 08)},
			{"net6.0-tizen", new DateOnly(2024, 11, 08)},
			{"net6.0-tvos", new DateOnly(2024, 11, 08)},
			{"net6.0-windows", new DateOnly(2024, 11, 08)},
		}.ToImmutableDictionary();

		public static TargetFrameworkCheckResponse GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit? timeframeUnit = null, byte? timeframeAmount = null) {
			DateOnly forecastedDateToCompare;
			if (timeframeUnit.HasValue && timeframeAmount.HasValue) {
				forecastedDateToCompare = timeframeUnit switch {
					TimeframeUnit.Day => DateOnly.FromDateTime(DateTime.UtcNow).AddDays(timeframeAmount.GetValueOrDefault()),
					TimeframeUnit.Week => DateOnly.FromDateTime(DateTime.UtcNow).AddDays(7 * timeframeAmount.GetValueOrDefault()),
					TimeframeUnit.Month => DateOnly.FromDateTime(DateTime.UtcNow).AddMonths(timeframeAmount.GetValueOrDefault()),
					TimeframeUnit.Year => DateOnly.FromDateTime(DateTime.UtcNow).AddYears(timeframeAmount.GetValueOrDefault()),
					_ => throw new ArgumentOutOfRangeException(nameof(timeframeUnit), timeframeUnit, null),
				};
			}
			else {
				forecastedDateToCompare = DateOnly.FromDateTime(DateTime.UtcNow);
			}

			return new TargetFrameworkCheckResponse(TargetFrameworksWithEndOfLifeDate.Where(tfm => tfm.Value <= forecastedDateToCompare)
			                                                                         .Select(tfm => tfm.Key)
			                                                                         .OrderBy(tfm => tfm).ToImmutableArray());
		}

		public static TargetFrameworkCheckResponse CheckTargetFrameworkForEndOfLife(string rawTfm) {
			if (string.IsNullOrWhiteSpace(rawTfm)) {
				throw new ArgumentNullException(nameof(rawTfm));
			}

			string[] tfms = rawTfm.Split(';', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

			if (tfms.Length == 0) {
				throw new ArgumentException($"No Target Framework Monikers could be found in string {rawTfm}");
			}

			string[] endOfLifeTargetFrameworks = tfms.Where(IsSingularTfmEol).ToArray();

			return new TargetFrameworkCheckResponse(endOfLifeTargetFrameworks.ToImmutableArray());
		}

		/// <summary>Determine if a singular Target Framework Moniker is currently end of life.</summary>
		/// <param name="tfm">The singular Target Framework Moniker to check for (eg. net45, netcoreapp2.1)</param>
		/// <returns></returns>
		/// <exception cref="TargetFrameworkUnknownException">Thrown when the TFM is not currently registered by the application.</exception>
		private static bool IsSingularTfmEol(string tfm) {
			if (TargetFrameworksWithEndOfLifeDate.ContainsKey(tfm)) {
				return TargetFrameworksWithEndOfLifeDate[tfm].HasValue && TargetFrameworksWithEndOfLifeDate[tfm]!.Value <= DateOnly.FromDateTime(DateTime.UtcNow);
			}

			throw new TargetFrameworkUnknownException($"I do not have TFM '{tfm}' in my registry. If this is a valid TFM, please log an issue on GitHub at https://github.com/Doug-Murphy/EndOfLifeApi/issues/new.");
		}
	}
}

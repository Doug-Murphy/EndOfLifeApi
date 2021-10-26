﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace EndOfLifeApi.Helpers {
	public class TargetFrameworkEndOfLifeHelper {
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
			{"net5.0", null},
			{"net5.0-android", null},
			{"net5.0-ios", null},
			{"net5.0-macos", null},
			{"net5.0-tvos", null},
			{"net5.0-watchos", null},
			{"net5.0-windows", null},

			//.NET 6 TFMs taken from https://github.com/dotnet/designs/blob/main/accepted/2021/net6.0-tfms/net6.0-tfms.md
			{"net6.0", null},
			{"net6.0-android", null},
			{"net6.0-ios", null},
			{"net6.0-macos", null},
			{"net6.0-maccatalyst", null},
			{"net6.0-tizen", null},
			{"net6.0-tvos", null},
			{"net6.0-windows", null},
		}.ToImmutableDictionary();
	}
}

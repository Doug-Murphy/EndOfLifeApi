using EndOfLifeApi.Enums;
using EndOfLifeApi.Exceptions;
using EndOfLifeApi.Helpers;
using EndOfLifeApi.Models;
using NUnit.Framework;
using System;

namespace EndOfLifeApi.Tests.Unit {
	[Parallelizable(ParallelScope.All)]
	public class TargetFrameworkEndOfLifeHelperTests {
		[Test(Description = "When given a singular TFM that is currently EOL, it determines that it is EOL.")]
		public void TargetFrameworkThatIsEolCorrectlyShowsEol() {
			var tfmToUse = "net45";

			TargetFrameworkCheckResponse eolResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfmToUse);

			CollectionAssert.IsNotEmpty(eolResults.EndOfLifeTargetFrameworks);
			CollectionAssert.Contains(eolResults.EndOfLifeTargetFrameworks, "net45");
		}

		[Test(Description = "When given a singular TFM that is not EOL, it determines that it is not EOL.")]
		public void TargetFrameworkThatIsNotEolCorrectlyShowsNotEol() {
			var tfmToUse = "net6.0";

			TargetFrameworkCheckResponse eolResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfmToUse);

			CollectionAssert.IsEmpty(eolResults.EndOfLifeTargetFrameworks);
		}

		[Test(Description = "When given two TFM's where both are EOL, it determines that they are both EOL.")]
		public void TwoTargetFrameworksWhenBothAreEolCorrectlyShowsEol() {
			var tfmToUse = "net45;netcoreapp2.1";

			TargetFrameworkCheckResponse eolResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfmToUse);

			CollectionAssert.IsNotEmpty(eolResults.EndOfLifeTargetFrameworks);
			CollectionAssert.Contains(eolResults.EndOfLifeTargetFrameworks, "net45");
			CollectionAssert.Contains(eolResults.EndOfLifeTargetFrameworks, "netcoreapp2.1");
		}

		[Test(Description = "When given two TFM's where only one is EOL, it determines that the correct one is EOL.")]
		public void TwoTargetFrameworksWhenOneIsEolCorrectlyShowsEol() {
			var tfmToUse = "net45;net6.0";

			TargetFrameworkCheckResponse eolResults = TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfmToUse);

			CollectionAssert.IsNotEmpty(eolResults.EndOfLifeTargetFrameworks);
			CollectionAssert.Contains(eolResults.EndOfLifeTargetFrameworks, "net45");
			CollectionAssert.DoesNotContain(eolResults.EndOfLifeTargetFrameworks, "net6.0");
		}

		[Test(Description = "When given no TFM's, an exception is thrown.")]
		public void EmptyStringTargetFrameworkThrowsArgumentNullException() {
			var tfmToUse = "";

			Assert.Throws<ArgumentNullException>(() => TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfmToUse));
		}

		[Test(Description = "When given an empty multiple TFM, an exception is thrown.")]
		public void EmptyMultipleTargetFrameworksThrowsArgumentException() {
			var tfmToUse = ";";

			Assert.Throws<ArgumentException>(() => TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfmToUse));
		}

		[Test(Description = "When given a valid format TFM, but the TFM is not currently registered, an exception is thrown.")]
		public void UnregisteredTargetFrameworksThrowsArgumentException() {
			var tfmToUse = "net60";

			Assert.Throws<TargetFrameworkUnknownException>(() => TargetFrameworkEndOfLifeHelper.CheckTargetFrameworkForEndOfLife(tfmToUse));
		}

		[Test(Description = "When getting all EOL TFM's not forecasting, the list is not empty.")]
		public void GettingAllEndOfLifeTargetFrameworksWithoutForecastingReturnsNonEmptyList() {
			TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers();

			CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
			//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
		}

		[Test(Description = "When getting all EOL TFM's with forecasting day, the list is not empty.")]
		public void GettingAllEndOfLifeTargetFrameworksWithDayForecastingReturnsNonEmptyList() {
			TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit.Day, 1);

			CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
			//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
		}

		[Test(Description = "When getting all EOL TFM's with forecasting week, the list is not empty.")]
		public void GettingAllEndOfLifeTargetFrameworksWithWeekForecastingReturnsNonEmptyList() {
			TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit.Week, 1);

			CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
			//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
		}

		[Test(Description = "When getting all EOL TFM's with forecasting month, the list is not empty.")]
		public void GettingAllEndOfLifeTargetFrameworksWithMonthForecastingReturnsNonEmptyList() {
			TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit.Month, 1);

			CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
			//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
		}

		[Test(Description = "When getting all EOL TFM's with forecasting year, the list is not empty.")]
		public void GettingAllEndOfLifeTargetFrameworksWithYearForecastingReturnsNonEmptyList() {
			TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit.Year, 1);

			CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
			//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
		}

		[Test(Description = "When getting all EOL TFM's with forecasting, but the forecasting arguments are not all set, the list is not empty.")]
		public void GettingAllEndOfLifeTargetFrameworksWithIncompleteForecastingReturnsNonEmptyList() {
			TargetFrameworkCheckResponse allCurrentEolTfms = TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit.Year);

			CollectionAssert.IsNotEmpty(allCurrentEolTfms.EndOfLifeTargetFrameworks);
			//intentionally not asserting on number of TFMs since that is date-driven and mocking date isn't important enough for this to be honest
		}

		[Test(Description = "When getting all EOL TFM's with invalid forecasting, but the timeframe unit is invalid, an exception is thrown.")]
		public void GettingAllEndOfLifeTargetFrameworksWithInvalidForecastingThrowsException() {
			Assert.Throws<ArgumentOutOfRangeException>(() => TargetFrameworkEndOfLifeHelper.GetAllEndOfLifeTargetFrameworkMonikers((TimeframeUnit)5, 1));
		}
	}
}

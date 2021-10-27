using System.Collections.Immutable;

namespace EndOfLifeApi.Models {
	public record TargetFrameworkCheckResponse(ImmutableArray<string> EndOfLifeTargetFrameworks);
}

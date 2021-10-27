using System.Text.Json.Serialization;

namespace EndOfLifeApi.Enums {
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum TimeframeUnit {
		Day,
		Week,
		Month,
		Year,
	}
}

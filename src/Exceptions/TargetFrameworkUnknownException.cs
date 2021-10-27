using System;

namespace EndOfLifeApi.Exceptions {
	public class TargetFrameworkUnknownException : Exception {
		public TargetFrameworkUnknownException(string message) : base(message) {
		}
	}
}

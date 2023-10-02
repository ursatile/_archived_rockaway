using System.Reflection;

namespace Rockaway.WebApp.Services {
	public class SystemStatus {
		public string Message { get; set; } = Assembly.GetEntryAssembly()?.FullName ?? "(assembly name not available)";
		public DateTimeOffset SystemTime { get; set; } = DateTimeOffset.Now;
	}

	public class StatusReporter {
		public SystemStatus Report() => new();
	}
}

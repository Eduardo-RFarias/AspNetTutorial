using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Mime;
using System.Text.Json;

namespace GarageRelation.API.Utils
{
	public static class Utilities
	{
		public static async Task GenerateHealthCheckCustomResponse(HttpContext context, HealthReport report)
		{
			var resultObj = new
			{
				status = report.Status.ToString(),
				checks = report.Entries.Select(entry => new
				{
					name = entry.Key,
					status = entry.Value.Status.ToString(),
					exception = entry.Value.Exception?.Message.ToString(),
					duration = entry.Value.Duration.ToString()
				})
			};

			string json = JsonSerializer.Serialize(resultObj);

			context.Response.ContentType = MediaTypeNames.Application.Json;
			await context.Response.WriteAsync(json);
		}
	}
}

using GarageRelation.Controllers.Repositories;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace GarageRelation.Utils.HealthChecks
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly MySqlRepository DatabaseContext;

        public DatabaseHealthCheck(MySqlRepository DatabaseContext)
        {
            this.DatabaseContext = DatabaseContext;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            bool canConnect = DatabaseContext.Database.CanConnect();

            return Task.FromResult(canConnect ? HealthCheckResult.Healthy() : HealthCheckResult.Unhealthy());
        }
    }
}

using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Infrastructure.Interception;

namespace PharmacySysyem.DAL
{
    public class PharmacyConfiguration : DbConfiguration
    {
        public PharmacyConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            DbInterception.Add(new PharmacyInterceptorTransientErrors());
            DbInterception.Add(new pharmacyInterceptorLogging());
        }

    }
}
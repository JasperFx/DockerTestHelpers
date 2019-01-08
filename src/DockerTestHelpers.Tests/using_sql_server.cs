using System.Threading.Tasks;
using Xunit;

namespace DockerTestHelpers.Tests
{
    public class using_sql_server
    {
        [Fact]
        public async Task try_loading_named_container()
        {
            var container = new SqlServerContainer("mssql-test");

            await container.Start(DockerServers.BuildDockerClient());
            
            
        }
    }
}
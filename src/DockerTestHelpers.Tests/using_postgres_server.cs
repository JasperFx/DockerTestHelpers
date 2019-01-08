using System.Threading.Tasks;
using Xunit;

namespace DockerTestHelpers.Tests
{
    public class using_postgres_server
    {
        [Fact]
        public async Task spin_an_image_up()
        {
            var container = new PostgresqlContainer("test-postgres");

            await container.Start();
        }
    }
}
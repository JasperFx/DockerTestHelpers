using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace DockerTestHelpers
{
    public class SqlServerContainer : DockerServer
    {
        private readonly int _port;
        private string _password;

        public SqlServerContainer(string containerName, int port = 1434, string password = "P@55w0rd") : base("microsoft/mssql-server-linux:latest", containerName)
        {
            _port = port;
            ConnectionString = $"Server=localhost,{port};User Id=sa;Password={password};Timeout=5";
            _password = password;
        }
        
        public string ConnectionString { get; }

        protected override async Task<bool> isReady()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    await conn.OpenAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override HostConfig ToHostConfig()
        {
            return new HostConfig
            {

                PortBindings = new Dictionary<string, IList<PortBinding>>
                {
                    {
                        "1433/tcp",
                        new List<PortBinding>
                        {
                            new PortBinding
                            {
                                HostPort = _port.ToString(),
                                HostIP = "127.0.0.1"
                            }
                        }
                    },
                },
            };
        }

        public override Config ToConfig()
        {
            return new Config
            {
                Env = new List<string> { "ACCEPT_EULA=Y", $"SA_PASSWORD={_password}", "MSSQL_PID=Developer" }
            };
        }
    }
}

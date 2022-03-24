using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LargeFileExtraction
{
    public class AppConfiguration
    {
        public DipConfiguration DIPConfiguration { get; set; }
        public PostgreSQLConnectionString PostgreSQLConnectionString { get; set; }
    }

    public class DipConfiguration
    {
        public string HostName { get; set; }
        public string PortNumber { get; set; }
        public string ProcessGroupId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class PostgreSQLConnectionString
    {
        public string ConnectionString { get; set; }
    }

   
}

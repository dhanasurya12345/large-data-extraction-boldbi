using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LargeFileExtraction
{
    public class AppConfiguration
    {
        public DipConfiguration DIPConfiguration { get; set; }
        public PostgreSQL PostgreSQL { get; set; }
    }

    public class DipConfiguration
    {
        public string HostName { get; set; }
        public string PortNumber { get; set; }
        public string ProcessGroupId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class PostgreSQL
    {
        public string ConnectionString { get; set; }
    }

   
}

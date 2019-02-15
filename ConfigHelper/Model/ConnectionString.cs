using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigHelper.Model
{
    public class ConnectionString
    {
        public bool MultipleActiveResultSets { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public int Timeout { get; set; }
        public bool Async { get; set; }
    }
}

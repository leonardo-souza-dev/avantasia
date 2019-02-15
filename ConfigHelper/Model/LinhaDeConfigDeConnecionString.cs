using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigHelper.Model
{
    public class LinhaDeConfigDeConnecionString
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
        public ConnectionString ConnectionStringObj { get; set; }
        public string ProviderName { get; set; }
    }
}

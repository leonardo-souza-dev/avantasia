using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConfigHelper.Base
{
    public class IisService
    {
        public SiteCollection ObterSites()
        {
            var iisManager = new ServerManager();

            return iisManager.Sites;
        }
    }
}

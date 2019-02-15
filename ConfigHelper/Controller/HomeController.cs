using Microsoft.Web.Administration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConfigHelper.Model;
using ConfigHelper.View;
using System.Configuration;

namespace ConfigHelper.Controller
{
    public class HomeController
    {
        public void MostraConexoesDeBanco()
        {
            var iisManager = new ServerManager();

            var indexSiteEscolhido = TelaDeEscolhaDeSiteView.Render(iisManager.Sites);

            var siteEscolhido = iisManager.Sites[indexSiteEscolhido];

            var caminhoFisicoDaConnectionString = ObterCaminhoFisicoDaConnectionString(siteEscolhido);

            var linhas = ObterLinhasDeConfigDeConnectionString(caminhoFisicoDaConnectionString);

            ExibirConexoesDeBancoView.Render(linhas);
        }

        private List<LinhaDeConfigDeConnecionString> ObterLinhasDeConfigDeConnectionString(string caminhoFisicoConnectionString)
        {
            XDocument doc = XDocument.Load(caminhoFisicoConnectionString);
            string jsonText = JsonConvert.SerializeXNode(doc);

            dynamic todoXML = JsonConvert.DeserializeObject<ExpandoObject>(jsonText);
            List<LinhaDeConfigDeConnecionString> conns = new List<LinhaDeConfigDeConnecionString>();

            foreach (var tagConnectionStringsPai in todoXML.connectionStrings.add)
            {
                var c = new LinhaDeConfigDeConnecionString();
                foreach (KeyValuePair<string, object> prop in tagConnectionStringsPai)
                {
                    var key = prop.Key;
                    var value = prop.Value;
                    if (key == "@name")
                        c.Name = value.ToString();
                    else if (key == "@connectionString")
                        c.ConnectionString = value.ToString();
                    else if (key == "@providerName")
                        c.ProviderName = value.ToString();
                }

                conns.Add(c);
            }

            return conns;
        }

        private string ObterCaminhoFisicoDaConnectionString(Site site)
        {
            var caminhoFisicoDaRaizDoSite = $"{site.Applications["/"].VirtualDirectories["/"].PhysicalPath}";
            var caminhoFisicoDoWebConfig = $"{caminhoFisicoDaRaizDoSite}\\Web.config";

            var sitesConnectionStringNaPastaConfigChave = ConfigurationManager.AppSettings["sitesConnectionStringNaPastaConfig"];

            var sitesConnectionStringNaPastaConfig = sitesConnectionStringNaPastaConfigChave.Split(',');

            var caminhoFisicoConnectionString = string.Empty;
            if (sitesConnectionStringNaPastaConfig.Contains(site.Name))
            {
                string[] linhasWebConfig = null;
                try
                {
                    linhasWebConfig = File.ReadAllLines(caminhoFisicoDoWebConfig);
                }
                catch (FileNotFoundException ex)
                {
                    Console.Clear();
                    throw new Exception("Web.config não encontrado.");
                }

                var linhaConnectionString = linhasWebConfig.FirstOrDefault(x => x.Contains("connectionStrings configSource="));
                var pos = linhaConnectionString.IndexOf("Config");
                var conexao = linhaConnectionString.Substring(pos + 8).Substring(0, linhaConnectionString.Substring(pos + 8).Length - 4);

                caminhoFisicoConnectionString = $@"{caminhoFisicoDaRaizDoSite}\Configs\{conexao}";
            }

            return caminhoFisicoConnectionString;
        }
    }
}

using ConfigHelper.Base;
using ConfigHelper.View;
using Microsoft.Web.Administration;
using ConfigHelper.Base;

namespace ConfigHelper.Controllers
{
    public class HomeController : Controller
    {
        private readonly IisService _iisService;
        private readonly ConfigService _configService;

        public HomeController()
        {
            _iisService = new IisService();
            _configService = new ConfigService();
        }

        public void TelaDeEscolhaDeSite()
        {
            var sites = _iisService.ObterSites();

            var indexSiteEscolhido = TelaDeEscolhaDeSiteView.Render(sites);

            ExibirConexoesDeBanco(sites[indexSiteEscolhido]);
        }

        public void ExibirConexoesDeBanco(Site siteEscolhido)
        {
            var linhas = _configService.ObterLinhasDeConfigDeConnectionStringPublic(siteEscolhido);

            ExibirConexoesDeBancoView.Render(linhas);
        }

        public Tela Listar()
        {
            var sites = _iisService.ObterSites();

            return View();
        }
    }
}

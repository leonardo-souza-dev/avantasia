using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigHelper.Base;

namespace ConfigHelper.View
{
    public static class TelaDeEscolhaDeSiteView
    {
        public static int Render(SiteCollection sites)
        {
            ImprimeTelaDeEscolha(sites, out int posicaoY);

            ObterSiteEscolhido(posicaoY, out int indexSiteEscolhido);

            return indexSiteEscolhido;
        }

        private static void ImprimeTelaDeEscolha(SiteCollection sites, out int y)
        {
            var iisManager = new ServerManager();
            sites = iisManager.Sites;
            y = 0;
            foreach (var s in sites)
            {
                Console.SetCursorPosition(3, y);
                Console.Write($"  {s.Name}");
                y++;
            }

            y = 0;
            Console.SetCursorPosition(0, y);
            Console.Write("x");
        }

        private static void ObterSiteEscolhido(int posicaoY, out int indexSiteEscolhido)
        {
            indexSiteEscolhido = posicaoY;
            var teclouEnter = false;

            while (!teclouEnter)
            {
                var tecla = Console.ReadKey();
                if (tecla.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, indexSiteEscolhido);
                    Console.Write(" ");
                    indexSiteEscolhido++;
                    Console.SetCursorPosition(0, indexSiteEscolhido);
                    Console.Write("x");
                }
                else if (tecla.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, indexSiteEscolhido);
                    Console.Write(" ");
                    indexSiteEscolhido--;
                    Console.SetCursorPosition(0, indexSiteEscolhido);
                    Console.Write("x");
                }
                else if (tecla.Key == ConsoleKey.Enter)
                {
                    teclouEnter = true;
                }
            }
        }
    }
}

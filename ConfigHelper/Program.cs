using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigHelper.Controllers;

namespace ConfigHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            new HomeController().TelaDeEscolhaDeSite();

            Console.ReadLine();
        }
    }
}

using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigHelper.Base;
using ConfigHelper.Model;

namespace ConfigHelper.View
{
    public static class ExibirConexoesDeBancoView
    {
        public static void Render(List<LinhaDeConfigDeConnecionString> conns)
        {
            Console.Clear();

            foreach (var connn in conns)
            {
                var con = connn.ConnectionString;

                var linhasDeConectionStrings = new List<LinhaDeConfigDeConnecionString>();

                if (con.Contains("MultipleActiveResultSets"))
                {
                    var conn = new ConnectionString();

                    var connProps = con.Split(';');

                    foreach (var za in connProps)
                    {
                        var zgg = za.Split('=');
                        var propp = zgg[0];

                        if (propp == "MultipleActiveResultSets")
                            conn.MultipleActiveResultSets = Convert.ToBoolean(zgg[1]);
                        else if (propp == "database")
                            conn.Database = zgg[1];
                        else if (propp == "server")
                            conn.Server = zgg[1];
                        else if (propp == "user id")
                            conn.UserId = zgg[1];
                        else if (propp == "password")
                            conn.Password = zgg[1];
                    }

                    var linhaDeConnectionString = new LinhaDeConfigDeConnecionString();
                    linhaDeConnectionString.Name = connn.Name;
                    linhaDeConnectionString.ConnectionStringObj = conn;
                    linhaDeConnectionString.ProviderName = connn.ProviderName;

                    linhasDeConectionStrings.Add(linhaDeConnectionString);

                    Console.WriteLine($"name: {linhaDeConnectionString.Name}, database: {linhaDeConnectionString.ConnectionStringObj.Database}");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp
{
    internal class TelaPrincipal
    {
        public string MostrarMenu()
        {
            Console.WriteLine("Controle de Bar");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Digite 1 para o menu de Garçons");
            Console.WriteLine("Digite 2 para o menu de Produtos");
            Console.WriteLine("Digite 3 para o menu de Mesas");
            Console.WriteLine("Digite 4 para o menu de Contas");

            Console.WriteLine("Digite s para Sair");
            string opcao = Console.ReadLine();
            return opcao;
        }
    }
}

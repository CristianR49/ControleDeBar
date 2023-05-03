using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.Garcons;
using ControleDeBar.ConsoleApp.Mesas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.Mesas
{
    internal class TelaMesa : TelaBase
    {
        RepositorioMesa repositorioMesa;

        public TelaMesa(RepositorioMesa repositorioMesa)
        {
            this.repositorioMesa = repositorioMesa;
            this.repositorioBase = repositorioMesa;
            this.nomeEntidade = "Mesa";
            this.sufixo = "s";
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.WriteLine("Digite o numero: ");
            string numero = Console.ReadLine();

            Mesa mesa = new Mesa(numero, true);
            return mesa;
        }
        protected override void MostrarTabela(ArrayList registros)
        {
            Console.WriteLine($"{"Id",-2}| {"Numero",-15} | {"Está livre",-15}");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            foreach (Mesa mesa in registros)
            {
                string status = mesa.estaLivre ? "Desocupada" : "Ocupada";
                Console.WriteLine($"{mesa.id,-2}| {mesa.numero,-15} | {status,-15}");
            }
        }
    }
}

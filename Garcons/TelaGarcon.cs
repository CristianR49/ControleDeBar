using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.Garcons
{
    internal class TelaGarcon : TelaBase
    {
        RepositorioGarcon repositorioGarcon;

        public TelaGarcon(RepositorioGarcon repositorioGarcon)
        {
            this.repositorioGarcon = repositorioGarcon;
            this.repositorioBase = repositorioGarcon;
            this.nomeEntidade = "Garçon";
            this.sufixo = "s";
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.WriteLine("Digite o nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o login: ");
            string login = Console.ReadLine();
            Console.WriteLine("Digite a senha: ");
            string senha = Console.ReadLine();

            Garcon garcon = new Garcon(nome, login, senha);
            return garcon;
        }
        protected override void MostrarTabela(ArrayList registros)
        {
            Console.WriteLine($"{ "Id", -2}| {"Nome",-15}| {"Login",-15}");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            foreach (Garcon garcon in registros)
            {
                Console.WriteLine($"{garcon.id,-2}| {garcon.nome,-15}| {garcon.login,-15}");
            }
        }
    }
}

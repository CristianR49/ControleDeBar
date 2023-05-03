using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.Produtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.Produtos
{
    internal class TelaProduto : TelaBase
    {
        RepositorioProduto repositorioProduto;

        public TelaProduto(RepositorioProduto repositorioProduto)
        {
            this.repositorioProduto = repositorioProduto;
            this.repositorioBase = repositorioProduto;
            this.nomeEntidade = "Produto";
            this.sufixo = "s";
        }

        protected override EntidadeBase ObterRegistro()
        {
            Console.WriteLine("Digite o nome: ");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o preço: ");

            double preco = 0;
            bool valorInvalido;
            do
            {
                valorInvalido = false;

                try
                {
                    preco = Convert.ToDouble(Console.ReadLine());
                }
                catch (FormatException)
                {
                    valorInvalido = true;
                    MostrarMensagem("O campo \"preço\" é obrigatório", ConsoleColor.Red);
                    Console.WriteLine("Digite o preço: ");
                }
                if (preco < 0)
                {
                    MostrarMensagem("Preço não pode ter valor negativo", ConsoleColor.Red);
                    valorInvalido = true;
                }

            } while (valorInvalido);


            Produto Produto = new Produto(nome, preco);
            return Produto;
        }
        protected override void MostrarTabela(ArrayList registros)
        {
            Console.WriteLine($"{"Id",-2}| {"Nome",-15}| {"Preco",-15}");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            foreach (Produto produto in registros)
            {
                Console.WriteLine($"{produto.id,-2}| {produto.nome,-15}| {produto.preco,-15}");
            }
        }

    }
}

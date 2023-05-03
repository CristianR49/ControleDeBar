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
    internal class Produto : EntidadeBase
    {
        public string nome;
        public double preco;

        public Produto(string nome, double preco)
        {
            this.nome = nome;
            this.preco = preco;
        }
        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Produto Produto = (Produto)registroAtualizado;

            this.nome = Produto.nome;
            this.preco = Produto.preco;
        }
        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();
            if (string.IsNullOrEmpty(nome.Trim()))
                erros.Add("O campo \"nome\" é obrigatório");
            if (preco < 0)
                erros.Add("Preço não pode ser negativo");
            return erros;
        }
    }
}

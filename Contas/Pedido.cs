using ControleDeBar.ConsoleApp.Garcons;
using ControleDeBar.ConsoleApp.Produtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.Contas
{
    internal class Pedido
    {
        public Produto produto;
        public Garcon garcon;
        public int quantidade;
        public int id;

        public Pedido(Produto produto, Garcon garcon, int quantidade)
        {
            this.produto = produto;
            this.garcon = garcon;
            this.quantidade = quantidade;
        }

    }
}

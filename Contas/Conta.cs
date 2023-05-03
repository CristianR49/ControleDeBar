using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.Contas;
using ControleDeBar.ConsoleApp.Garcons;
using ControleDeBar.ConsoleApp.Mesas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.Contas
{
    internal class Conta : EntidadeBase
    {
        public Mesa mesa;
        public Garcon garcon;
        public ArrayList pedidos = new ArrayList();
        public bool estaAberta;
        public DateTime dia;

        public Conta(Mesa mesa, Garcon garcon, DateTime dia)
        {
            this.mesa = mesa;
            this.garcon = garcon;
            this.dia = dia;
        }
        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Conta conta = (Conta)registroAtualizado;

            this.estaAberta = false;
        }
        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();
            if (garcon == null)
                erros.Add("O campo \"garçon\" é obrigatório");
            if (mesa == null)
                erros.Add("O campo \"mesa\" é obrigatório");
            return erros;
        }
    }
}

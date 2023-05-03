using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.Mesas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.Mesas
{
    internal class Mesa : EntidadeBase
    {
        public string numero;
        public bool estaLivre;

        public Mesa(string numero, bool estaLivre)
        {
            this.numero = numero;
            this.estaLivre = estaLivre;
        }
        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Mesa garcon = (Mesa)registroAtualizado;

            this.numero = garcon.numero;
        }
        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();
            if (string.IsNullOrEmpty(numero.Trim()))
                erros.Add("O campo \"numero\" é obrigatório");
            return erros;
        }
    }
}

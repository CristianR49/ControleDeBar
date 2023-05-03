using ControleDeBar.ConsoleApp.Compartilhado;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.Garcons
{
    internal class Garcon : EntidadeBase
    {
        public string nome;
        public string login;
        public string senha;

        public Garcon(string nome, string login, string senha)
        {
            this.nome = nome;
            this.login = login;
            this.senha = senha;
        }
        public override void AtualizarInformacoes(EntidadeBase registroAtualizado)
        {
            Garcon garcon = (Garcon)registroAtualizado;

            this.nome = garcon.nome;
            this.login = garcon.login;
            this.senha = garcon.senha;
        }
        public override ArrayList Validar()
        {
            ArrayList erros = new ArrayList();
            if (string.IsNullOrEmpty(nome.Trim()))
                erros.Add("O campo \"nome\" é obrigatório");
            if (string.IsNullOrEmpty(login.Trim()))
                erros.Add("O campo \"login\" é obrigatório");
            if (string.IsNullOrEmpty(senha.Trim()))
                erros.Add("O campo \"senha\" é obrigatório");
            return erros;
        }
    }
}

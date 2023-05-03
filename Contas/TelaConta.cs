using ControleDeBar.ConsoleApp.Compartilhado;
using ControleDeBar.ConsoleApp.Contas;
using ControleDeBar.ConsoleApp.Garcons;
using ControleDeBar.ConsoleApp.Mesas;
using ControleDeBar.ConsoleApp.Produtos;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ControleDeBar.ConsoleApp.Contas
{
    internal class TelaConta : TelaBase
    {
        RepositorioConta repositorioConta;
        RepositorioGarcon repositorioGarcon;
        RepositorioMesa repositorioMesa;
        RepositorioProduto repositorioProduto;

        TelaGarcon telaGarcon;
        TelaMesa telaMesa;
        TelaProduto telaProduto;

        int contadorDeIdPedido = 0;

        public TelaConta(RepositorioConta repositorioConta, TelaGarcon telaGarcon, TelaMesa telaMesa, TelaProduto telaProduto, RepositorioGarcon repositorioGarcon, RepositorioMesa repositorioMesa, RepositorioProduto repositorioProduto)
        {
            this.repositorioConta = repositorioConta;
            this.repositorioBase = repositorioConta;
            this.repositorioGarcon = repositorioGarcon;
            this.repositorioMesa = repositorioMesa;
            this.repositorioProduto = repositorioProduto;
            this.telaGarcon = telaGarcon;
            this.telaMesa = telaMesa;
            this.telaProduto = telaProduto;
            this.nomeEntidade = "Conta";
            this.sufixo = "s";
        }

        public void AbrirConta()
        {
            MostrarCabecalho("Controle de Contas", "Abrir uma Conta");

            if (repositorioGarcon.TemRegistros() == false)
            {
                MostrarMensagem("Não há Garçon registrado", ConsoleColor.DarkYellow);
                return;
            }
            if (repositorioMesa.TemRegistros() == false)
            {
                MostrarMensagem("Não há Mesa registrada", ConsoleColor.DarkYellow);
                return;
            }

            ArrayList mesas = repositorioMesa.SelecionarTodos();
            bool haMesaLivre = false;
            foreach (Mesa m in mesas)
            {
                if (m.estaLivre == true)
                { haMesaLivre = true; }
            }
            if (haMesaLivre == false)
            {
                Console.WriteLine("Não há mesas livres");
                return;
            }

            if (repositorioProduto.TemRegistros() == false)
            {
                MostrarMensagem("Não há Produto registrado", ConsoleColor.DarkYellow);
                return;
            }

            Conta conta = ObterRegistro();

            if (TemErrosDeValidacao(conta))
            {
                AbrirConta();

                return;
            }
            conta.mesa.estaLivre = false;
            conta.estaAberta = true;
            repositorioConta.Inserir(conta);

            MostrarMensagem("Conta aberta com sucesso!", ConsoleColor.Green);
        }
        public void FecharConta()
        {

            MostrarCabecalho("Controle de Contas", "Fechar uma Conta");

            if (repositorioConta.TemRegistros() == false)
            {
                MostrarMensagem("Não há Conta registrada", ConsoleColor.DarkYellow);
                return;
            }

            ArrayList contas = repositorioConta.SelecionarTodos();

            bool haContasAbertas = false;
            foreach (Conta c in contas)
            {
                if (c.estaAberta == true)
                {
                    haContasAbertas = true;
                }
            }
            if (haContasAbertas == false)
            {
                MostrarMensagem("Não há Contas abertas", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine("Contas Registradas:");

            MostrarTabela(contas);
            Conta conta = (Conta)EncontrarRegistro("Digite o id do registro: ");

            conta.mesa.estaLivre = true;
            conta.estaAberta = false;

            MostrarMensagem("Conta fechada com sucesso", ConsoleColor.Green);
        }
        public void VisualizarContasAbertas(bool visualizarCabecalho, bool apertarEnter)
        {
            if (visualizarCabecalho == true)
                MostrarCabecalho("Controle de Contas", "Contas abertas:");

            if (repositorioConta.TemRegistros() == false)
            {
                MostrarMensagem("Não há Conta registrada", ConsoleColor.DarkYellow);
                return;
            }

            ArrayList contas = repositorioConta.SelecionarTodos();

            bool haContasAbertas = false;
            foreach (Conta conta in contas)
            {
                if (conta.estaAberta == true)
                {
                    haContasAbertas = true;
                }
            }
            if (haContasAbertas == false)
            {
                MostrarMensagem("Não há Contas abertas", ConsoleColor.DarkYellow);
                return;
            }

            ArrayList contasAbertas = new ArrayList();
            foreach (Conta conta in contas)
            {
                if (conta.estaAberta == true)
                    contasAbertas.Add(conta);
            }

            MostrarTabela(contasAbertas);

            if (apertarEnter == true)
                Console.ReadLine();

        }
        protected override Conta ObterRegistro()
        {

            Console.WriteLine("Selecione o Garçon\n");
            telaGarcon.VisualizarRegistros(false, false);
            Garcon garcon = (Garcon)telaGarcon.EncontrarRegistro("Digite o id do registro: ");

            Console.WriteLine("\nSelecione a Mesa\n");
            telaMesa.VisualizarRegistros(false, false);

            Mesa mesa;
            bool mesaLivre = true;
            do
            {
                mesa = (Mesa)telaMesa.EncontrarRegistro("Digite o id do registro: ");
                if (mesa.estaLivre == false)
                {
                    Console.WriteLine("Essa Mesa está ocupada! Selecione outra Mesa");
                    mesaLivre = false;
                }
            }
            while (mesaLivre == false);


            DateTime dia = DateTime.Now;

            Conta conta = new Conta(mesa, garcon, dia);
            return conta;
        }
        protected override void MostrarTabela(ArrayList registros)
        {
            Console.WriteLine($"{"Id",-2}| {"Garçon",-15}| {"Mesa",-15}| {"Status",-15} | {"Data",-15}");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            foreach (Conta conta in registros)
            {
                string status = conta.estaAberta ? "Aberto" : "Fechado";
                Console.WriteLine($"{conta.id,-2}| {conta.garcon.nome,-15}| {conta.mesa.numero,-15}| {status,-15}| {conta.dia,-15}");
            }
        }
        public void RegistrarPedido()
        {
            MostrarCabecalho("Controle de Contas", "Registrar Pedido");

            if (repositorioConta.TemRegistros() == false)
            {
                MostrarMensagem("Não há Conta registrada", ConsoleColor.DarkYellow);
                return;
            }
            if (repositorioProduto.TemRegistros() == false)
            {
                MostrarMensagem("Não há Produto registrado", ConsoleColor.DarkYellow);
                return;
            }
            if (repositorioGarcon.TemRegistros() == false)
            {
                MostrarMensagem("Não há Garçon registrado", ConsoleColor.DarkYellow);
                return;
            }

            ArrayList contas = repositorioConta.SelecionarTodos();

            bool haContasAbertas = false;
            foreach (Conta c in contas)
            {
                if (c.estaAberta == true)
                {
                    haContasAbertas = true;
                }
            }
            if (haContasAbertas == false)
            {
                MostrarMensagem("Não há Contas abertas", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine("Selecione a Conta\n");
            VisualizarContasAbertas(false, false);
            Conta conta = (Conta)EncontrarRegistro("Digite o id do registro: ");

            Console.WriteLine("\nSelecione o Produto\n");
            telaProduto.VisualizarRegistros(false, false);
            Produto produto = (Produto)telaProduto.EncontrarRegistro("Digite o id do registro: ");

            Console.WriteLine("\nSelecione o Garcon\n");
            telaGarcon.VisualizarRegistros(false, false);
            Garcon garcon = (Garcon)telaGarcon.EncontrarRegistro("Digite o id do registo: ");

            bool valorInvalido;
            int quantidade = 0;
            do
            {
                valorInvalido = false;
                Console.WriteLine("Digite a quantidade: ");
                try
                {
                    quantidade = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    MostrarMensagem("Valor inválido", ConsoleColor.Red);
                    valorInvalido = true;
                }
                if (quantidade < 0)
                {
                    Console.WriteLine("Quantidade não pode ter valor negativo");
                    valorInvalido = true;
                }
            } while (valorInvalido);

            Pedido pedido = new Pedido(produto, garcon, quantidade);

            contadorDeIdPedido++;
            pedido.id = contadorDeIdPedido;

            conta.pedidos.Add(pedido);

            MostrarMensagem("Pedido registrado com sucesso!", ConsoleColor.Green);
        }
        public void RemoverPedido()
        {
            MostrarCabecalho("Controle de Contas", "Remover Pedido");

            if (repositorioConta.TemRegistros() == false)
            {
                MostrarMensagem("Não há Conta registrada", ConsoleColor.DarkYellow);
                return;
            }

            bool haPedido = false;
            ArrayList contas = repositorioConta.SelecionarTodos();
            foreach (Conta c in contas)
            {
                if (c.pedidos.Count > 0)
                    haPedido = true;
            }
            if (haPedido == false)
            {
                Console.WriteLine("Não há Pedido registrado", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine("Selecione a conta\n");

            MostrarTabela(contas);

            Conta conta = (Conta)EncontrarRegistro("Digite o id do registo: ");

            Console.WriteLine("\nSelecione o Pedido a ser removido\n");

            ArrayList pedidos = conta.pedidos;

            VisualizarPedidos(pedidos);

            Console.WriteLine("\nSelecione o id do Pedido");
            int idSelecionado = Convert.ToInt32(Console.ReadLine());

            ExcluirPedido(pedidos, idSelecionado);

            MostrarMensagem("Pedido retirado com sucesso", ConsoleColor.Green);
        }
        public void VisualizarPedidos(ArrayList pedidos)
        {
            Console.WriteLine($"{"Id",-2}| {"Produto",-15}| {"Quantidade",-10}| {"Garçon",-15}");
            Console.WriteLine("---------------------------------------------------------------------------------------");
            foreach (Pedido pedido in pedidos)
            {
                Console.WriteLine($"{pedido.id,-2}| {pedido.produto.nome,-15}| {pedido.quantidade,-10}| {pedido.garcon.nome,-15}");
            }
        }

        public void ExcluirPedido(ArrayList pedidos, int id)
        {
            Pedido pedido = null;
            foreach (Pedido p in pedidos)
            {
                if (p.id == id)
                {
                    pedido = p;
                }
            }
            pedidos.Remove(pedido);
        }
        public void VisualizarFaturamentoTotalNoDia()
        {
            MostrarCabecalho("Controle de Contas", "Faturamento total");

            if (repositorioConta.TemRegistros() == false)
            {
                MostrarMensagem("Não há Conta registrada", ConsoleColor.DarkYellow);
                return;
            }

            Console.WriteLine("Digite a data: ");
            DateTime data = Convert.ToDateTime(Console.ReadLine());
            
            Console.WriteLine();

            int faturamentoTotal = 0;

            ArrayList contas = repositorioBase.SelecionarTodos();

            ArrayList mesas = repositorioMesa.SelecionarTodos();

            foreach (Mesa m in mesas)
            {
                foreach (Conta conta in contas)
                {
                    int totalMesa = 0;
                    if (conta.dia.Date == data.Date)
                        foreach (Pedido p in conta.pedidos)
                        {
                            totalMesa += (int)p.produto.preco * p.quantidade;
                            faturamentoTotal += (int)p.produto.preco * p.quantidade;
                        }
                    Console.WriteLine($"Mesa {conta.mesa.numero}: R${totalMesa}");
                }
            }

            Console.WriteLine("\nFaturamento total no dia: R$" + faturamentoTotal);

            Console.ReadLine();
        }
        public override string ApresentarMenu()
        {
            Console.Clear();

            Console.WriteLine($"Cadastro de Contas \n");

            Console.WriteLine($"Digite 1 para Abrir Conta");
            Console.WriteLine($"Digite 2 para Registrar um pedido");
            Console.WriteLine($"Digite 3 para Visualizar Contas abertas");
            Console.WriteLine($"Digite 4 para Fechar Conta");
            Console.WriteLine($"Digite 5 para Retirar Pedido");
            Console.WriteLine($"Digite 6 para Visualizar o faturamento do dia");

            Console.WriteLine("Digite s para Sair");

            string opcao = Console.ReadLine();

            return opcao;
        }
    }
}

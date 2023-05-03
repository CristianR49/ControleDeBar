using ControleDeBar.ConsoleApp.Contas;
using ControleDeBar.ConsoleApp.Garcons;
using ControleDeBar.ConsoleApp.Mesas;
using ControleDeBar.ConsoleApp.Produtos;

namespace ControleDeBar.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            TelaPrincipal telaPrincipal = new TelaPrincipal();

            RepositorioGarcon repositorioGarcon = new RepositorioGarcon();
            TelaGarcon telaGarcon = new TelaGarcon(repositorioGarcon);

            RepositorioProduto repositorioProduto = new RepositorioProduto();
            TelaProduto telaProduto = new TelaProduto(repositorioProduto);

            RepositorioMesa repositorioMesa = new RepositorioMesa();
            TelaMesa telaMesa = new TelaMesa(repositorioMesa);

            RepositorioConta repositorioConta = new RepositorioConta();
            TelaConta telaConta = new TelaConta(repositorioConta, telaGarcon, telaMesa, telaProduto, repositorioGarcon, repositorioMesa, repositorioProduto);

            CadastrarRegistros(repositorioGarcon, repositorioMesa, repositorioProduto);
            
            
            

            while (true)
            {
                Console.Clear();
                string opcaoMenuPrincipal;
                opcaoMenuPrincipal = telaPrincipal.MostrarMenu();

                while (opcaoMenuPrincipal == "1")
                {
                    string opcao;
                    opcao = telaGarcon.ApresentarMenu();
                    if (opcao == "1")
                    {
                        telaGarcon.InserirNovoRegistro();
                    }
                    if (opcao == "2")
                    {
                        telaGarcon.VisualizarRegistros(true, true);
                    }
                    if (opcao == "3")
                    {
                        telaGarcon.EditarRegistro();
                    }
                    if (opcao == "4")
                    {
                        telaGarcon.ExcluirRegistro();
                    }
                    if (opcao == "s")
                    {
                        break;
                    }
                }

                while (opcaoMenuPrincipal == "2")
                {
                    string opcao;
                    opcao = telaProduto.ApresentarMenu();
                    if (opcao == "1")
                    {
                        telaProduto.InserirNovoRegistro();
                    }
                    if (opcao == "2")
                    {
                        telaProduto.VisualizarRegistros(true, true);
                    }
                    if (opcao == "3")
                    {
                        telaProduto.EditarRegistro();
                    }
                    if (opcao == "4")
                    {
                        telaProduto.ExcluirRegistro();
                    }
                    if (opcao == "s")
                    {
                        break;
                    }
                }

                while (opcaoMenuPrincipal == "3")
                {
                    string opcao;
                    opcao = telaMesa.ApresentarMenu();
                    if (opcao == "1")
                    {
                        telaMesa.InserirNovoRegistro();
                    }
                    if (opcao == "2")
                    {
                        telaMesa.VisualizarRegistros(true, true);
                    }
                    if (opcao == "3")
                    {
                        telaMesa.EditarRegistro();
                    }
                    if (opcao == "4")
                    {
                        telaMesa.ExcluirRegistro();
                    }
                    if (opcao == "s")
                    {
                        break;
                    }
                }

                while (opcaoMenuPrincipal == "4")
                {
                    string opcao;
                    opcao = telaConta.ApresentarMenu();
                    if (opcao == "1")
                    {
                        telaConta.AbrirConta();
                    }
                    if (opcao == "2")
                    {
                        telaConta.RegistrarPedido();
                    }
                    if (opcao == "3")
                    {
                        telaConta.VisualizarContasAbertas(true, true);
                    }
                    if (opcao == "4")
                    {
                        telaConta.FecharConta();
                    }
                    if (opcao == "5")
                    {
                        telaConta.RemoverPedido();
                    }
                    if (opcao == "6") 
                    {
                        telaConta.VisualizarFaturamentoTotalNoDia();
                    }
                    if (opcao == "s")
                    {
                        break;
                    }
                }
                if (opcaoMenuPrincipal == "s")
                {
                    break;
                }
            }
        }
        private static void CadastrarRegistros(RepositorioGarcon repositorioGarcon, RepositorioMesa repositorioMesa, RepositorioProduto repositorioProduto)
        {
            string nome = "Juliano";
            string login = "Juli123";
            string senha = "123654";
            Garcon garcon = new Garcon(nome, login, senha);
            repositorioGarcon.Inserir(garcon);

            nome = "Cerveja";
            double preco = 10;
            Produto produto = new Produto(nome, preco);
            repositorioProduto.Inserir(produto);

            string numero = "1";
            Mesa mesa = new Mesa(numero, true);
            repositorioMesa.Inserir(mesa);
        }
    }
}
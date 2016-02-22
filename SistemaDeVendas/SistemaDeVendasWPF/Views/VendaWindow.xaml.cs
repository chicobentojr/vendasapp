using SistemaDeVendasWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SistemaDeVendasWPF.Views
{
    public partial class VendaWindow : Window
    {
        public static List<VendaProduto> Produtos = new List<VendaProduto>();
        public static int VendaId = 0;

        public VendaWindow()
        {
            InitializeComponent();
            this.carregarLayout();
        }

        public void carregarLayout()
        {
            cmbCliente.ItemsSource = Cliente.Listar();
            cmbProduto.ItemsSource = Produto.Listar();
            txtData.Text = DateTime.Now.ToShortDateString();
            txtQuantidade.Text = "";
        }

        public void atualizarGradeProdutos()
        {
            grdProdutos.ItemsSource = null;
            grdProdutos.ItemsSource = Produtos;
        }

        private void btnAdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = cmbCliente.SelectedItem as Cliente;
            DateTime data = DateTime.Parse(txtData.ToString());
            Produto produto = cmbProduto.SelectedItem as Produto;
            int quantidade = int.Parse(txtQuantidade.Text);

            int itemNumero = grdProdutos.Items.Count + 1;

            VendaWindow.Produtos.Add(new VendaProduto(new Venda(cliente.ClienteId,data),itemNumero,quantidade,produto));
            this.atualizarGradeProdutos();
            lblTotal.Text = String.Format("Total: R$ {0}",Produtos.Sum(p=>p.Preco));
        }

        private void btnFinalizarVenda_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = cmbCliente.SelectedItem as Cliente;
            DateTime data = DateTime.Parse(txtData.ToString());

            Venda venda = new Venda(cliente.ClienteId, data);

            venda = Venda.Inserir(venda);

            if (venda.VendaId != 0)
            {
                int vendaId = venda.VendaId;
                List<VendaProduto> produtosVendidos = new List<VendaProduto>();
                foreach (VendaProduto item in Produtos)
                {
                    item.VendaId = vendaId;

                    VendaProduto vendaProduto = VendaProduto.Inserir(item);
                    if (vendaProduto.VendaId == -1)
                    {
                        MessageBox.Show(String.Format("O produto {0} não tem {1} unidade(s) no estoque. Seu pedido não poderá ser concluído!", item.Produto.Descricao, item.Quantidade.ToString()));
                    }
                    else
                    {
                        produtosVendidos.Add(vendaProduto);
                    }
                }
                Produtos = produtosVendidos;
                this.atualizarGradeProdutos();
                if (Produtos.Count > 0)
                {
                    MessageBox.Show("Venda realizada com sucesso!");

                    if (!cliente.Vip && venda.Cliente.Vip)
                    {
                        MessageBox.Show("Parabéns! Você se tornou cliente VIP! Agora você tem 15% de desconto na suas compras");
                    }

                    this.carregarLayout();
                    Produtos = new List<VendaProduto>();
                }
                else
                {
                    MessageBox.Show("Nenhum Produto foi inserido!");
                }
            }
            else
            {
                MessageBox.Show("Ocorreu um erro na venda!");
            }
        }
    }
}

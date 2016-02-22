using SistemaDeVendasWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SistemaDeVendasWPF.Helpers;

namespace SistemaDeVendasWPF.Views
{
    public partial class VendaWindow : Window
    {
        public static List<VendaProduto> Produtos = new List<VendaProduto>();

        public VendaWindow()
        {
            Produtos = new List<VendaProduto>();
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
        public void atualizarTotais()
        {
            try
            {
                Cliente cliente = cmbCliente.SelectedItem as Cliente;
                if (cliente != null)
                {
                    int desconto = 0;
                    if (cliente.Vip)
                    {
                        desconto = 15;
                    }
                    else
                    {
                        List<Venda> vendas = Venda.Listar();
                        int quantidadeVendas = vendas.Where(v => v.ClienteId == cliente.ClienteId).Count();

                        desconto = quantidadeVendas >= 10 ? 10 : quantidadeVendas;
                    }
                    decimal total1 = Produtos.Sum(p => p.Preco);
                    decimal total2 = desconto > 0 ? total1 - total1 * desconto / 100 : total1;
                    lblTotal1.Text = String.Format("Total sem desconto: R$ {0}", total1.paraValorReal());
                    lblDesconto.Text = String.Format("Desconto: {0}%", desconto);
                    lblTotal2.Text = String.Format("Total com desconto: R$ {0}", total2.paraValorReal());
                }
            }
            catch
            {
                MessageBox.Show("Verifique se os dados estão preenchidos corretamente!", "Alerta");
            }
        }

        public void atualizarGradeProdutos()
        {
            grdProdutos.ItemsSource = null;
            grdProdutos.ItemsSource = Produtos;
        }

        private void btnAdicionarProduto_Click(object sender, RoutedEventArgs e)
        {
            try {
                Cliente cliente = cmbCliente.SelectedItem as Cliente;
                DateTime data = DateTime.Parse(txtData.ToString());
                Produto produto = cmbProduto.SelectedItem as Produto;
                int quantidade = int.Parse(txtQuantidade.Text);

                if (produto.QtdEstoque >= quantidade)
                {
                    int itemNumero = grdProdutos.Items.Count + 1;

                    VendaWindow.Produtos.Add(new VendaProduto(new Venda(cliente.ClienteId, data), itemNumero, quantidade, produto));
                    this.atualizarGradeProdutos();
                    this.atualizarTotais();
                }
                else
                {
                    MessageBox.Show("Não existe essa quantidade disponível no estoque!","Alerta");
                }
            }

            catch
            {
                MessageBox.Show("Verifique se os dados estão preenchidos corretamente!", "Alerta");
            }
        }

        private void btnFinalizarVenda_Click(object sender, RoutedEventArgs e)
        {
            try {
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
                            MessageBox.Show(String.Format("O produto {0} não tem {1} unidade(s) no estoque. Seu pedido não poderá ser concluído!", item.Produto.Descricao, item.Quantidade.ToString()),"Alerta");
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
                        MessageBox.Show("Venda realizada com sucesso!", "Aviso");

                        if (!cliente.Vip && venda.Cliente.Vip)
                        {
                            MessageBox.Show("Parabéns! Você se tornou cliente VIP! Agora você tem 15% de desconto na suas compras","Aviso");
                        }

                        this.carregarLayout();
                        Produtos = new List<VendaProduto>();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum Produto foi inserido!", "Alerta");
                    }
                }
                else
                {
                    MessageBox.Show("Ocorreu um erro na venda!","Alerta");
                }
            }

            catch
            {
                MessageBox.Show("Verifique se os dados estão preenchidos corretamente!", "Alerta");
            }
        }
    }
}

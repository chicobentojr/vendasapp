using SistemaDeVendasWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SistemaDeVendasWPF.Helpers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SistemaDeVendasWPF.Views
{
    /// <summary>
    /// Interaction logic for ListagemWindow.xaml
    /// </summary>
    public partial class ListagemWindow : Window
    {
        public ListagemWindow()
        {
            InitializeComponent();
            this.carregarLayout();
        }

        public void carregarLayout()
        {
            cmbCliente.ItemsSource = Cliente.Listar();
        }

        private void btnVendaPeriodo_Click(object sender, RoutedEventArgs e)
        {
            DateTime inicio = DateTime.Parse(txtDataInicio.ToString());
            DateTime fim = DateTime.Parse(txtDataFim.ToString());

            List<Venda> vendas = Listagem.ListarPorPeriodo(inicio.ToString("yyyy-MM-dd"), fim.ToString("yyyy-MM-dd"));
            
            grdResultado.ItemsSource = from v in vendas select new {
                Id = v.VendaId,
                Cliente = v.Cliente.Nome,
                Data = v.Data.ToShortDateString(),
                Total1 = v.Total1.paraValorReal(),
                Desconto = v.Desconto + "%",
                Total2 = v.Total2.paraValorReal()
            };

            grdResultado.Columns[3].Header = "Total sem desconto";
            grdResultado.Columns[5].Header = "Total com desconto";

            lblResultado.Content = String.Format("Resultado: Vendas de {0} até {1}",inicio.ToShortDateString(),fim.ToShortDateString());
        }

        private void btnMaisVendidos_Click(object sender, RoutedEventArgs e)
        {
            int quantidade = int.Parse(txtQuantidade.Text);
            
            List<Listagem.MaisVendidos> maisVendidos = Listagem.ListarMaisVendidos(quantidade);

            int i = 1;

            grdResultado.ItemsSource = from mv in maisVendidos
                                       select new
                                       {
                                           Indíce = i++,
                                           Produto = mv.Produto,
                                           Fabricante = mv.Fabricante,
                                           Vendidos = mv.Vendidos
                                       };

            grdResultado.Columns[3].Header = "Quantidade de Vendidos";

            lblResultado.Content = String.Format("Resultado: {0}° Produto(s) mais vendido(s)",quantidade);
        }

        private void btnClientesVips_Click(object sender, RoutedEventArgs e)
        {
            grdResultado.ItemsSource = Listagem.ListarClientesVips();

            grdResultado.Columns[0].Header = "Id";

            lblResultado.Content = "Resultado: Clientes VIP's";
        }

        private void cmbCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cliente cliente = cmbCliente.SelectedItem as Cliente;
            int clienteId = cliente.ClienteId;

            List<Venda> vendas = Listagem.ListarPorCliente(clienteId);

            grdResultado.ItemsSource = from v in vendas
                                       select new
                                       {
                                           Id = v.VendaId,
                                           Cliente = v.Cliente.Nome,
                                           Data = v.Data.ToShortDateString(),
                                           Total1 = v.Total1.paraValorReal(),
                                           Desconto = v.Desconto + "%",
                                           Total2 = v.Total2.paraValorReal()
                                       };

            grdResultado.Columns[3].Header = "Total sem desconto";
            grdResultado.Columns[5].Header = "Total com desconto";

            lblResultado.Content = String.Format("Resultado: Vendas do Cliente {0}",cliente.Nome);
        }
    }
}

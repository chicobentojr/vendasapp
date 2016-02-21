using SistemaDeVendasWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            DateTime inicio =  DateTime.Parse(txtDataInicio.ToString());
            DateTime fim = DateTime.Parse(txtDataFim.ToString());

            grdResultado.ItemsSource = Listagem.ListarPorPeriodo(inicio.ToString("yyyy-MM-dd"), fim.ToString("yyyy-MM-dd"));
            lblResultado.Content = String.Format("Resultado: Vendas de {0} até {1}",inicio.ToShortDateString(),fim.ToShortDateString());
        }

        private void btnMaisVendidos_Click(object sender, RoutedEventArgs e)
        {
            int quantidade = int.Parse(txtQuantidade.Text);

            grdResultado.ItemsSource = Listagem.ListarMaisVendidos(quantidade);
            lblResultado.Content = String.Format("Resultado: {0}° Produto(s) mais vendido(s)",quantidade);
        }

        private void btnClientesVips_Click(object sender, RoutedEventArgs e)
        {
            grdResultado.ItemsSource = Listagem.ListarClientesVips();
            lblResultado.Content = "Resultado: Clientes VIP's";
        }

        private void cmbCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Cliente cliente = cmbCliente.SelectedItem as Cliente;
            int clienteId = cliente.ClienteId;
            grdResultado.ItemsSource = Listagem.ListarPorCliente(clienteId);
            lblResultado.Content = String.Format("Resultado: Vendas do Cliente {0}",cliente.Nome);
        }
    }
}

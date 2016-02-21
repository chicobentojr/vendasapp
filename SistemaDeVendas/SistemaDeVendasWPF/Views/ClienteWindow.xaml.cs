using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SistemaDeVendasWPF.Models;
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
    /// Interaction logic for Cliente.xaml
    /// </summary>
    public partial class ClienteWindow : Window
    {
        public ClienteWindow()
        {
            InitializeComponent();

            this.carregarClientes();
        }

        public void carregarClientes()
        {
            List<Cliente> clientes = Cliente.Listar();
            grdClientes.ItemsSource = clientes;
            cmbEdtCliente.ItemsSource = clientes;
            cmbDelCliente.ItemsSource = clientes;
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNome.Text;
            bool vip = chkVip.IsEnabled;

            Cliente cliente = new Cliente(nome, vip);

            cliente = Cliente.Inserir(cliente);

            if(cliente.ClienteId != 0)
            {
                this.carregarClientes();
                MessageBox.Show("Cliente cadastrado com sucesso!");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

            Cliente cliente = cmbEdtCliente.SelectedItem as Cliente;
            
            cliente.Nome = txtEdtNome.Text;
            cliente.Vip = chkEdtVip.IsEnabled;

            cliente = Cliente.Editar(cliente);

            if (cliente.ClienteId != 0)
            {
                this.carregarClientes();
                MessageBox.Show("Cliente atualizado com sucesso!");
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Cliente cliente = cmbDelCliente.SelectedItem as Cliente;

            cliente = Cliente.Excluir(cliente);

            if (cliente.ClienteId != 0)
            {
                this.carregarClientes();
                MessageBox.Show("Cliente excluído com sucesso!");
            }

        }
    }
}

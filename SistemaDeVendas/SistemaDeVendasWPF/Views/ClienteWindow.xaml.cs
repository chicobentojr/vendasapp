using SistemaDeVendasWPF.Models;
using System.Collections.Generic;
using System.Windows;

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
            bool vip = chkVip.IsChecked.Value;

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
            cliente.Vip = chkEdtVip.IsChecked.Value;

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

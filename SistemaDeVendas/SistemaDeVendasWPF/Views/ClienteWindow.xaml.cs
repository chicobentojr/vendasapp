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
            grdClientes.ItemsSource = Cliente.Listar();
        }
    }
}

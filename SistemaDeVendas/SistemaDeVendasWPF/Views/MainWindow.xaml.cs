using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SistemaDeVendasWPF.Views;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaDeVendasWPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            new ClienteWindow().Show();
        }

        private void btnFabricantes_Click(object sender, RoutedEventArgs e)
        {
            new FabricanteWindow().Show();
        }

        private void btnProdutos_Click(object sender, RoutedEventArgs e)
        {
            new ProdutoWIndow().Show();
        }
    }
}

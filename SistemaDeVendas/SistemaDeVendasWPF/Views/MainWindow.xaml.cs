using System.Windows;

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

        private void btnListagens_Click(object sender, RoutedEventArgs e)
        {
            new ListagemWindow().Show();
        }
    }
}

using SistemaDeVendasWPF.Models;
using System.Collections.Generic;
using System.Windows;

namespace SistemaDeVendasWPF.Views
{
    /// <summary>
    /// Interaction logic for FabricanteWindow.xaml
    /// </summary>
    public partial class FabricanteWindow : Window
    {
        public FabricanteWindow()
        {
            InitializeComponent();
            this.carregarFabricantes();
        }

        public void carregarFabricantes()
        {
            List<Fabricante> fabricantes = Fabricante.Listar();
            grdFabricantes.ItemsSource = fabricantes;
            cmbEdtFabricante.ItemsSource = fabricantes;
            cmbDelFabricante.ItemsSource = fabricantes;
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            string descricao = txtDescricao.Text;
            
            Fabricante fabricante = new Fabricante(descricao);

            fabricante = Fabricante.Inserir(fabricante);

            if (fabricante.FabricanteId!= 0)
            {
                this.carregarFabricantes();
                MessageBox.Show("Fabricante cadastrado com sucesso!");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

            Fabricante fabricante = cmbEdtFabricante.SelectedItem as Fabricante;

            fabricante.Descricao = txtEdtDescricao.Text;

            fabricante = Fabricante.Editar(fabricante);

            if (fabricante.FabricanteId != 0)
            {
                this.carregarFabricantes();
                MessageBox.Show("Fabricante atualizado com sucesso!");
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            Fabricante fabricante = cmbDelFabricante.SelectedItem as Fabricante;

            fabricante = Fabricante.Excluir(fabricante);

            if (fabricante.FabricanteId != 0)
            {
                this.carregarFabricantes();
                MessageBox.Show("Cliente excluído com sucesso!");
            }

        }
    }
}

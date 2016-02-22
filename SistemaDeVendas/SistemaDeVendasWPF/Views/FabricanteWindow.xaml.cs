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
            try
            {
                string descricao = txtDescricao.Text;

                Fabricante fabricante = new Fabricante(descricao);

                fabricante = Fabricante.Inserir(fabricante);

                if (fabricante.FabricanteId != 0)
                {
                    this.carregarFabricantes();
                    MessageBox.Show("Fabricante cadastrado com sucesso!", "Aviso");
                }
            }
            catch
            {
                MessageBox.Show("Verifique se os dados estão preenchidos corretamente!", "Alerta");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

            try
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
            catch
            {
                MessageBox.Show("Verifique se os dados estão preenchidos corretamente!", "Alerta");
            }
        }

        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Fabricante fabricante = cmbDelFabricante.SelectedItem as Fabricante;

                fabricante = Fabricante.Excluir(fabricante);

                if (fabricante.FabricanteId != 0)
                {
                    this.carregarFabricantes();
                    MessageBox.Show("Cliente excluído com sucesso!","Alerta");
                }
            }
            catch
            {
                MessageBox.Show("Verifique se os dados estão preenchidos corretamente!", "Alerta");
            }
        }
    }
}

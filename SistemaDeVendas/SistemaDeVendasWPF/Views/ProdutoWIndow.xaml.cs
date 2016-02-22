using SistemaDeVendasWPF.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace SistemaDeVendasWPF.Views
{
    /// <summary>
    /// Interaction logic for ProdutoWIndow.xaml
    /// </summary>
    public partial class ProdutoWIndow : Window
    {
        public ProdutoWIndow()
        {
            InitializeComponent();
            this.carregarProdutos();
        }

        public void carregarProdutos()
        {
            List<Produto> produtos = Produto.Listar();
            grdProdutos.ItemsSource = produtos;
            cmbEdtProduto.ItemsSource = produtos;
            cmbDelProduto.ItemsSource = produtos;

            List<Fabricante> fabricantes = Fabricante.Listar();
            cmbFabricante.ItemsSource = fabricantes;
            cmbEdtFabricante.ItemsSource = fabricantes;
        }

        private void btnCadastrar_Click(object sender, RoutedEventArgs e)
        {
            try {
                string descricao = txtDescricao.Text;
                int fabricanteId = (cmbFabricante.SelectedItem as Fabricante).FabricanteId;
                int qtdEstoque = int.Parse(txtQtdEstoque.Text);
                decimal preco = decimal.Parse(txtPreco.Text,new CultureInfo("pt-BR"));

                Produto produto = new Produto(descricao, fabricanteId, qtdEstoque, preco);

                produto = Produto.Inserir(produto);

                if (produto.ProdutoId != 0)
                {
                    this.carregarProdutos();
                    MessageBox.Show("Produto cadastrado com sucesso!","Aviso");
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
                Produto produto = cmbEdtProduto.SelectedItem as Produto;

                produto.Descricao = txtEdtDescricao.Text;
                produto.FabricanteId = (cmbEdtFabricante.SelectedItem as Fabricante).FabricanteId;
                produto.QtdEstoque = int.Parse(txtEdtQtdEstoque.Text);
                produto.Preco = decimal.Parse(txtEdtPreco.Text,new CultureInfo("pt-BR"));

                produto = Produto.Editar(produto);

                if (produto.ProdutoId != 0)
                {
                    this.carregarProdutos();
                    MessageBox.Show("Produto atualizado com sucesso!","Aviso");
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
                Produto produto = cmbDelProduto.SelectedItem as Produto;

                produto = Produto.Excluir(produto);

                if (produto.ProdutoId != 0)
                {
                    this.carregarProdutos();
                    MessageBox.Show("Produto excluído com sucesso!","Aviso");
                }
            }
            catch
            {
                MessageBox.Show("Verifique se os dados estão preenchidos corretamente!","Alerta");
            }
        }
    }
}

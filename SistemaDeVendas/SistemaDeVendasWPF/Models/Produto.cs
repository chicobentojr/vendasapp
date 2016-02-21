using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendasWPF.Models
{
    public class Produto
    {
        public const string MEDIA_TYPE = "application/x-www-form-urlencoded";

        public override string ToString()
        {
            return this.Descricao;
        }

        public Produto(string descricao,int fabricanteId, int qtdEstoque, double preco)
        {
            this.Descricao = descricao;
            this.FabricanteId = fabricanteId;
            this.QtdEstoque = qtdEstoque;
            this.Preco = preco;
        }

        public int ProdutoId { get; set; }
        public string Descricao { get; set; }
        public int FabricanteId { get; set; }
        public int QtdEstoque { get; set; }
        public double Preco { get; set; }

        public Fabricante Fabricante { get; set; }


        public static List<Produto> Listar()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            HttpResponseMessage resposta = httpClient.GetAsync("produto").Result;

            List<Produto> produtos = JsonConvert.DeserializeObject<List<Produto>>(resposta.Content.ReadAsStringAsync().Result);

            return produtos;
        }

        public static Produto Inserir(Produto produto)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            string jsonProduto = JsonConvert.SerializeObject(produto);
            StringContent content = new StringContent(jsonProduto, Encoding.UTF8, MEDIA_TYPE);

            HttpResponseMessage resposta = httpClient.PostAsync("produto", produto.jsonObject()).Result;

            produto = JsonConvert.DeserializeObject<Produto>(resposta.Content.ReadAsStringAsync().Result);

            return produto;
        }

        public static Produto Editar(Produto produto)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            string jsonCliente = JsonConvert.SerializeObject(produto);
            StringContent content = new StringContent(jsonCliente, Encoding.UTF8, MEDIA_TYPE);

            HttpResponseMessage resposta = httpClient.PutAsync("produto/" + produto.ProdutoId, produto.jsonObject()).Result;

            produto = JsonConvert.DeserializeObject<Produto>(resposta.Content.ReadAsStringAsync().Result);

            return produto;
        }

        public static Produto Excluir(Produto produto)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            HttpResponseMessage resposta = httpClient.DeleteAsync("produto/" + produto.ProdutoId).Result;

            produto = JsonConvert.DeserializeObject<Produto>(resposta.Content.ReadAsStringAsync().Result);

            return produto;
        }


        public FormUrlEncodedContent jsonObject()
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("ProdutoId",this.ProdutoId.ToString()),
                new KeyValuePair<string,string>("Descricao",this.Descricao),
                new KeyValuePair<string,string>("FabricanteId",this.FabricanteId.ToString()),
                new KeyValuePair<string,string>("QtdEstoque",this.QtdEstoque.ToString()),
                new KeyValuePair<string,string>("Preco",this.Preco.ToString())
            });
        }
    }
}

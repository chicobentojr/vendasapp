using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendasWPF.Models
{
    public class Listagem
    {
        public class MaisVendidos
        {
            public int ProdutoId { get; set; }
            public string Produto { get; set; }
            public string Fabricante { get; set; }
            public int Vendidos { get; set; }
        }


        public static List<Venda> ListarPorCliente(int clienteId)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            HttpResponseMessage resposta = httpClient.GetAsync("listagem/vendasporcliente/"+clienteId).Result;

            List<Venda> vendas = JsonConvert.DeserializeObject<List<Venda>>(resposta.Content.ReadAsStringAsync().Result);

            return vendas;
        }

        public static List<Venda> ListarPorPeriodo(string inicio, string fim)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            HttpResponseMessage resposta = httpClient.GetAsync("listagem/vendasporperiodo/" + inicio + "/"+ fim).Result;

            List<Venda> vendas = JsonConvert.DeserializeObject<List<Venda>>(resposta.Content.ReadAsStringAsync().Result);

            return vendas;
        }

        public static List<MaisVendidos> ListarMaisVendidos(int quantidade)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            HttpResponseMessage resposta = httpClient.GetAsync("listagem/produtosmaisvendidos/" + quantidade).Result;

            List<MaisVendidos> maisVendidos = JsonConvert.DeserializeObject<List<MaisVendidos>>(resposta.Content.ReadAsStringAsync().Result);

            return maisVendidos;
        }

        public static List<Cliente> ListarClientesVips()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            HttpResponseMessage resposta = httpClient.GetAsync("listagem/clientesvip").Result;

            List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(resposta.Content.ReadAsStringAsync().Result);

            return clientes;
        }
    }
}

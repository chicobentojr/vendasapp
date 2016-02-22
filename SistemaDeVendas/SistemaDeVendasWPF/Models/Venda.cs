using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendasWPF.Models
{
    public class Venda
    {
        public int VendaId { get; set; }
        public int ClienteId { get; set; }
        public DateTime Data { get; set; }
        public double Total1 { get; set; }
        public double Total2 { get; set; }
        public double Desconto { get; set; }
        public Cliente Cliente { get; set; }
        
        public Venda(int clienteId,DateTime data)
        {
            this.ClienteId = clienteId;
            this.Data = data;
        }

        public const string MEDIA_TYPE = "application/x-www-form-urlencoded";
        
        public static List<Venda> Listar()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);
            
            HttpResponseMessage resposta = httpClient.GetAsync("venda").Result;

            List<Venda> vendas = JsonConvert.DeserializeObject<List<Venda>>(resposta.Content.ReadAsStringAsync().Result);

            return vendas;
        }

        public static Venda Inserir(Venda venda)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            string jsonVenda = JsonConvert.SerializeObject(venda);
            StringContent content = new StringContent(jsonVenda, Encoding.UTF8, MEDIA_TYPE);

            HttpResponseMessage resposta = httpClient.PostAsync("venda", venda.jsonObject()).Result;

            venda = JsonConvert.DeserializeObject<Venda>(resposta.Content.ReadAsStringAsync().Result);

            return venda;
        }

        public static Venda Editar(Venda venda)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            string jsonVenda = JsonConvert.SerializeObject(venda);
            StringContent content = new StringContent(jsonVenda, Encoding.UTF8, MEDIA_TYPE);

            HttpResponseMessage resposta = httpClient.PutAsync("venda/" + venda.VendaId, venda.jsonObject()).Result;

            venda = JsonConvert.DeserializeObject<Venda>(resposta.Content.ReadAsStringAsync().Result);

            return venda;
        }

        //public static Cliente Excluir(Cliente cliente)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

        //    HttpResponseMessage resposta = httpClient.DeleteAsync("cliente/" + cliente.ClienteId).Result;

        //    cliente = JsonConvert.DeserializeObject<Cliente>(resposta.Content.ReadAsStringAsync().Result);

        //    return cliente;
        //}


        public FormUrlEncodedContent jsonObject()
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("VendaId",this.VendaId.ToString()),
                new KeyValuePair<string,string>("ClienteId",this.ClienteId.ToString()),
                new KeyValuePair<string,string>("Data",this.Data.ToString("yyyy-MM-dd")),
                new KeyValuePair<string,string>("Total1",this.Total1.ToString()),
                new KeyValuePair<string,string>("Total2",this.Total2.ToString()),
                new KeyValuePair<string,string>("Desconto",this.Desconto.ToString())
            });
        }
    }
}

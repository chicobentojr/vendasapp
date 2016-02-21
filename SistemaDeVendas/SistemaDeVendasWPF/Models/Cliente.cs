using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendasWPF.Models
{
    public class Cliente
    {
        public const string MEDIA_TYPE = "application/x-www-form-urlencoded";

        public override string ToString()
        {
            return this.Nome;
        }

        public Cliente(string nome, bool vip)
        {
            this.Nome = nome;
            this.Vip = vip;
        }

        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public bool Vip { get; set; }

        public static List<Cliente> Listar()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            HttpResponseMessage resposta = httpClient.GetAsync("cliente").Result;

            List<Cliente> clientes = JsonConvert.DeserializeObject<List<Cliente>>(resposta.Content.ReadAsStringAsync().Result);

            return clientes;
        }

        public static Cliente Inserir(Cliente cliente)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            string jsonCliente = JsonConvert.SerializeObject(cliente);
            StringContent content = new StringContent(jsonCliente,Encoding.UTF8,MEDIA_TYPE);
            
            HttpResponseMessage resposta = httpClient.PostAsync("cliente",cliente.jsonObject()).Result;

            cliente = JsonConvert.DeserializeObject<Cliente>(resposta.Content.ReadAsStringAsync().Result);

            return cliente;
        }

        public static Cliente Editar(Cliente cliente)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            string jsonCliente = JsonConvert.SerializeObject(cliente);
            StringContent content = new StringContent(jsonCliente, Encoding.UTF8, MEDIA_TYPE);

            HttpResponseMessage resposta = httpClient.PutAsync("cliente/"+cliente.ClienteId, cliente.jsonObject()).Result;

            cliente = JsonConvert.DeserializeObject<Cliente>(resposta.Content.ReadAsStringAsync().Result);

            return cliente;
        }

        public static Cliente Excluir(Cliente cliente)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);
            
            HttpResponseMessage resposta = httpClient.DeleteAsync("cliente/" + cliente.ClienteId).Result;

            cliente = JsonConvert.DeserializeObject<Cliente>(resposta.Content.ReadAsStringAsync().Result);

            return cliente;
        }


        public FormUrlEncodedContent jsonObject()
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("ClienteId",this.ClienteId.ToString()),
                new KeyValuePair<string,string>("Nome",this.Nome),
                new KeyValuePair<string,string>("Vip",this.Vip.ToString())
            });
        }
    }
}

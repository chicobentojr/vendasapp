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
    }
}

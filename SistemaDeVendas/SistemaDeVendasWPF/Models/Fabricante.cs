using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendasWPF.Models
{
    public class Fabricante
    {
        public const string MEDIA_TYPE = "application/x-www-form-urlencoded";

        public override string ToString()
        {
            return this.Descricao;
        }

        public Fabricante(string descricao)
        {
            this.Descricao = descricao;
        }

        public int FabricanteId { get; set; }
        public string Descricao { get; set; }

        public static List<Fabricante> Listar()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            HttpResponseMessage resposta = httpClient.GetAsync("fabricante").Result;

            List<Fabricante> fabricantes = JsonConvert.DeserializeObject<List<Fabricante>>(resposta.Content.ReadAsStringAsync().Result);

            return fabricantes;
        }

        public static Fabricante Inserir(Fabricante fabricante)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            string jsonFabricante = JsonConvert.SerializeObject(fabricante);
            StringContent content = new StringContent(jsonFabricante, Encoding.UTF8, MEDIA_TYPE);

            HttpResponseMessage resposta = httpClient.PostAsync("fabricante", fabricante.jsonObject()).Result;

            fabricante = JsonConvert.DeserializeObject<Fabricante>(resposta.Content.ReadAsStringAsync().Result);

            return fabricante;
        }

        public static Fabricante Editar(Fabricante fabricante)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            string jsonCliente = JsonConvert.SerializeObject(fabricante);
            StringContent content = new StringContent(jsonCliente, Encoding.UTF8, MEDIA_TYPE);

            HttpResponseMessage resposta = httpClient.PutAsync("fabricante/" + fabricante.FabricanteId, fabricante.jsonObject()).Result;

            fabricante = JsonConvert.DeserializeObject<Fabricante>(resposta.Content.ReadAsStringAsync().Result);

            return fabricante;
        }

        public static Fabricante Excluir(Fabricante fabricante)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            HttpResponseMessage resposta = httpClient.DeleteAsync("fabricante/" + fabricante.FabricanteId).Result;

            fabricante = JsonConvert.DeserializeObject<Fabricante>(resposta.Content.ReadAsStringAsync().Result);

            return fabricante;
        }


        public FormUrlEncodedContent jsonObject()
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("FabricanteId",this.FabricanteId.ToString()),
                new KeyValuePair<string,string>("Descricao",this.Descricao)
            });
        }
    }
}

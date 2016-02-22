using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SistemaDeVendasWPF.Models
{
    public class VendaProduto
    {
        public int VendaId { get; set; }
        public int ItemNumero { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public Produto Produto { get; set; }
        public Venda Venda { get; set; }

        public VendaProduto(Venda venda,int itemNumero,int quantidade,Produto produto)
        {
            this.VendaId = venda.VendaId;
            this.ItemNumero = itemNumero;
            this.ProdutoId = produto.ProdutoId;
            this.Quantidade = quantidade;
            this.Preco = quantidade * produto.Preco;
            this.Produto = produto;
            this.Venda = venda;
        }

        public const string MEDIA_TYPE = "application/x-www-form-urlencoded";

        public static VendaProduto Inserir(VendaProduto vendaProduto)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Properties.Settings.Default.UrlBase);

            string jsonVendaProduto = JsonConvert.SerializeObject(vendaProduto);
            StringContent content = new StringContent(jsonVendaProduto, Encoding.UTF8, MEDIA_TYPE);

            HttpResponseMessage resposta = httpClient.PostAsync("vendaproduto/" + vendaProduto.VendaId, vendaProduto.jsonObject()).Result;
            
            vendaProduto = JsonConvert.DeserializeObject<VendaProduto>(resposta.Content.ReadAsStringAsync().Result);

            return vendaProduto;
        }

        public FormUrlEncodedContent jsonObject()
        {
            return new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("VendaId",this.VendaId.ToString()),
                new KeyValuePair<string,string>("ItemNumero",this.ItemNumero.ToString("yyyy-MM-dd")),
                new KeyValuePair<string,string>("ProdutoId",this.ProdutoId.ToString()),
                new KeyValuePair<string,string>("Quantidade",this.Quantidade.ToString()),
                new KeyValuePair<string,string>("Preco",this.Preco.ToString().Replace(',','.'))
            });
        }
    }
}

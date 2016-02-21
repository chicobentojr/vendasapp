using SistemaDeVendasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SistemaDeVendasAPI.Controllers
{
    public class ListagemController : ApiController
    {
        private DbVendasDataContext contexto = new DbVendasDataContext();

        [HttpGet]
        public List<Venda> VendasPorPeriodo(DateTime parametro1,DateTime parametro2)
        {
            return contexto.Vendas.Where(v => v.Data >= parametro1 && v.Data <= parametro2).ToList();
        }
        [HttpGet]
        public List<Venda> VendasPorCliente(int parametro1)
        {
            return contexto.Vendas.Where(v => v.ClienteId == parametro1).ToList();
        }
        [HttpGet]
        public List<spProdutosMaisVendidosPorMesResult> ProdutosMaisVendidos(int parametro1)
        {
            return contexto.spProdutosMaisVendidosPorMes(parametro1).ToList();
        }
        [HttpGet]
        public List<vwClientesVip> ClientesVip()
        {
            return contexto.vwClientesVips.ToList();
        }
    }
}

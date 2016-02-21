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
        public IHttpActionResult VendasPorPeriodo(DateTime parametro1,DateTime parametro2)
        {
            return Json(contexto.Vendas.Where(v => v.Data >= parametro1 && v.Data <= parametro2).ToList());
        }
        [HttpGet]
        public IHttpActionResult VendasPorCliente(int parametro1)
        {
            return Json(contexto.Vendas.Where(v => v.ClienteId == parametro1).ToList());
        }
        [HttpGet]
        public IHttpActionResult ProdutosMaisVendidos(int parametro1)
        {
            return Json(contexto.spProdutosMaisVendidosPorMes(parametro1).ToList());
        }
        [HttpGet]
        public IHttpActionResult ClientesVip()
        {
            return Json(contexto.vwClientesVips.ToList());
        }
    }
}

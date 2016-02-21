using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SistemaDeVendasAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "VendaApi",
                routeTemplate: "api/venda/{vendaId}",
                defaults: new { controller = "Venda", fabricanteId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ProdutoApi",
                routeTemplate: "api/produto/{produtoId}",
                defaults: new { controller = "Produto", fabricanteId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ClienteApi",
                routeTemplate: "api/cliente/{clienteId}",
                defaults: new { controller = "Cliente", fabricanteId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "FabricanteApi",
                routeTemplate: "api/fabricante/{fabricanteId}",
                defaults: new { controller = "Fabricante", fabricanteId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

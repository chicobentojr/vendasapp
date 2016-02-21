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
                name: "ListagemApi",
                routeTemplate: "api/listagem/{action}/{parametro1}/{parametro2}",
                defaults: new { controller = "Listagem", parametro1 = RouteParameter.Optional, parametro2 = RouteParameter.Optional }
            );


            config.Routes.MapHttpRoute(
                name: "VendaProdutoApi",
                routeTemplate: "api/vendaproduto/{vendaId}/{itemNumero}",
                defaults: new { controller = "VendaProduto", itemNumero = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "VendaApi",
                routeTemplate: "api/venda/{vendaId}",
                defaults: new { controller = "Venda", vendaId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ProdutoApi",
                routeTemplate: "api/produto/{produtoId}",
                defaults: new { controller = "Produto", produtoId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "ClienteApi",
                routeTemplate: "api/cliente/{clienteId}",
                defaults: new { controller = "Cliente", clienteId = RouteParameter.Optional }
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

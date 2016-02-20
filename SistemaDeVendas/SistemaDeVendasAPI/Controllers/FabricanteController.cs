using SistemaDeVendasAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SistemaDeVendasAPI.Controllers
{
    public class FabricanteController : ApiController
    {
        private DbVendasDataContext contexto = new DbVendasDataContext();

        [HttpGet]
        public List<Fabricante> Get()
        {
            return contexto.Fabricantes.ToList();
        }
        [HttpGet]
        public Fabricante Get(int fabricanteId)
        {
            return contexto.Fabricantes.FirstOrDefault(f => f.FabricanteId == fabricanteId);
        }
        [HttpPost]
        public Fabricante Post(Fabricante fabricante)
        {
            contexto.Fabricantes.InsertOnSubmit(fabricante);
            contexto.SubmitChanges();
            return fabricante;
        }
        [HttpPut]
        public Fabricante Put(int fabricanteId, Fabricante fabricante)
        {
            Fabricante temp = contexto.Fabricantes.FirstOrDefault(f => f.FabricanteId == fabricanteId);

            temp.Descricao = fabricante.Descricao;

            contexto.SubmitChanges();

            return temp;
        }
        [HttpDelete]
        public Fabricante Delete(int fabricanteId)
        {
            Fabricante temp = contexto.Fabricantes.FirstOrDefault(f => f.FabricanteId == fabricanteId);

            contexto.Fabricantes.DeleteOnSubmit(temp);

            contexto.SubmitChanges();

            return temp;
        }
    }
}

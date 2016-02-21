using SistemaDeVendasAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SistemaDeVendasAPI.Controllers
{
    public class FabricanteController : ApiController
    {
        private DbVendasDataContext contexto = new DbVendasDataContext();

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(contexto.Fabricantes.ToList());
        }

        [HttpGet]
        public IHttpActionResult Get(int fabricanteId)
        {
            return Json(contexto.Fabricantes.FirstOrDefault(f => f.FabricanteId == fabricanteId));
        }

        [HttpPost]
        public IHttpActionResult Post(Fabricante fabricante)
        {
            contexto.Fabricantes.InsertOnSubmit(fabricante);
            contexto.SubmitChanges();
            return Json(fabricante);
        }

        [HttpPut]
        public IHttpActionResult Put(int fabricanteId, Fabricante fabricante)
        {
            Fabricante temp = contexto.Fabricantes.FirstOrDefault(f => f.FabricanteId == fabricanteId);

            temp.Descricao = fabricante.Descricao;

            contexto.SubmitChanges();

            return Json(temp);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int fabricanteId)
        {
            Fabricante temp = contexto.Fabricantes.FirstOrDefault(f => f.FabricanteId == fabricanteId);

            contexto.Fabricantes.DeleteOnSubmit(temp);

            contexto.SubmitChanges();

            return Json(temp);
        }
    }
}

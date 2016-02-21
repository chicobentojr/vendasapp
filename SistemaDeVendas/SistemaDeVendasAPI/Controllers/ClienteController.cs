using SistemaDeVendasAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SistemaDeVendasAPI.Controllers
{
    public class ClienteController : ApiController
    {
        private DbVendasDataContext contexto = new DbVendasDataContext();

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(contexto.Clientes.ToList());
        }

        [HttpGet]
        public IHttpActionResult Get(int clienteId)
        {
            return Json(contexto.Clientes.FirstOrDefault(c => c.ClienteId == clienteId));
        }

        [HttpPost]
        public IHttpActionResult Post(Cliente cliente)
        {
            contexto.Clientes.InsertOnSubmit(cliente);
            contexto.SubmitChanges();
            return Json(cliente);
        }

        [HttpPut]
        public IHttpActionResult Put(int clienteId, Cliente cliente)
        {
            Cliente temp = contexto.Clientes.FirstOrDefault(c => c.ClienteId == clienteId);

            temp.Nome = cliente.Nome;
            temp.Vip = cliente.Vip;

            contexto.SubmitChanges();

            return Json(temp);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int clienteId)
        {
            Cliente temp = contexto.Clientes.FirstOrDefault(c => c.ClienteId == clienteId);

            contexto.Clientes.DeleteOnSubmit(temp);

            contexto.SubmitChanges();

            return Json(temp);
        }
    }
}

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
        public List<Cliente> Get()
        {
            return contexto.Clientes.ToList();
        }

        [HttpGet]
        public Cliente Get(int clienteId)
        {
            return contexto.Clientes.FirstOrDefault(c => c.ClienteId == clienteId);
        }

        [HttpPost]
        public Cliente Post(Cliente cliente)
        {
            contexto.Clientes.InsertOnSubmit(cliente);
            contexto.SubmitChanges();
            return cliente;
        }

        [HttpPut]
        public Cliente Put(int clienteId, Cliente cliente)
        {
            Cliente temp = contexto.Clientes.FirstOrDefault(c => c.ClienteId == clienteId);

            temp.Nome = cliente.Nome;
            temp.Vip = cliente.Vip;

            contexto.SubmitChanges();

            return temp;
        }

        [HttpDelete]
        public Cliente Delete(int clienteId)
        {
            Cliente temp = contexto.Clientes.FirstOrDefault(c => c.ClienteId == clienteId);

            contexto.Clientes.DeleteOnSubmit(temp);

            contexto.SubmitChanges();

            return temp;
        }
    }
}

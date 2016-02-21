using SistemaDeVendasAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SistemaDeVendasAPI.Controllers
{
    public class VendaController : ApiController
    {
        private DbVendasDataContext contexto = new DbVendasDataContext();

        [HttpGet]
        public List<Venda> Get()
        {
            return contexto.Vendas.ToList();
        }

        [HttpGet]
        public Venda Get(int vendaId)
        {
            return contexto.Vendas.FirstOrDefault(v => v.VendaId == vendaId);
        }

        [HttpPost]
        public Venda Post(Venda venda)
        {
            int? vendaId = 0;
            contexto.spVendaCadastro(venda.ClienteId, venda.Data, ref vendaId);

            if (vendaId != 0)
            {
                venda = contexto.Vendas.FirstOrDefault(v => v.VendaId == vendaId);
                return venda;
            }
            return null;
        }

        [HttpPut]
        public Venda Put(int vendaId, Venda venda)
        {
            Venda temp = contexto.Vendas.FirstOrDefault(v => v.VendaId == vendaId);

            temp.ClienteId = venda.ClienteId;
            temp.Data = venda.Data;
            temp.Total1 = venda.Total1;
            temp.Total2 = venda.Total2;
            temp.Desconto = venda.Desconto;

            contexto.SubmitChanges();

            return temp;
        }

        [HttpDelete]
        public Venda Delete(int vendaId)
        {
            Venda temp = contexto.Vendas.FirstOrDefault(v => v.VendaId == vendaId);

            contexto.Vendas.DeleteOnSubmit(temp);

            contexto.SubmitChanges();

            return temp;
        }
    }
}

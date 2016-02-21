using SistemaDeVendasAPI.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;

namespace SistemaDeVendasAPI.Controllers
{
    public class VendaProdutoController : ApiController
    {
        private DbVendasDataContext contexto = new DbVendasDataContext();

        [HttpGet]
        public IHttpActionResult Get(int vendaId)
        {
            return Json(contexto.VendaProdutos.Where(vp=>vp.VendaId == vendaId).ToList());
        }

        [HttpGet]
        public IHttpActionResult Get(int vendaId,int itemNumero)
        {
            return Json(contexto.VendaProdutos.FirstOrDefault(v => v.VendaId == vendaId));
        }

        [HttpPost]
        public IHttpActionResult Post(int vendaId, VendaProduto vendaProduto)
        {
            int? itemNumero = 0;
            try {
                contexto.spVendaProdutoCadastro(vendaId, vendaProduto.ProdutoId, vendaProduto.Quantidade, ref itemNumero);

                if (itemNumero != 0)
                {
                    vendaProduto = contexto.VendaProdutos.FirstOrDefault(vp => vp.VendaId == vendaId && vp.ItemNumero == itemNumero);
                    return Json(vendaProduto);
                }
            }
            catch (SqlException ex)
            {
                if(ex.Number == 50000)
                {
                    vendaProduto.VendaId = -1;
                    return Json(vendaProduto);
                }
            }
            return null;
        }

        [HttpPut]
        public IHttpActionResult Put(int vendaId, int itemNumero, VendaProduto vendaProduto)
        {
            VendaProduto temp = contexto.VendaProdutos.FirstOrDefault(vp => vp.VendaId == vendaId && vp.ItemNumero == itemNumero);

            temp.ProdutoId = vendaProduto.ProdutoId;
            temp.Quantidade = vendaProduto.Quantidade;
            temp.Preco = vendaProduto.Preco;

            contexto.SubmitChanges();

            return Json(temp);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int vendaId,int itemNumero)
        {
            VendaProduto temp = contexto.VendaProdutos.FirstOrDefault(vp => vp.VendaId == vendaId && vp.ItemNumero == itemNumero);

            contexto.VendaProdutos.DeleteOnSubmit(temp);

            contexto.SubmitChanges();

            return Json(temp);
        }
    }
}

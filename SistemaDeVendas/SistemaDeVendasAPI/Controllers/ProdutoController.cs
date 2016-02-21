using SistemaDeVendasAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace SistemaDeVendasAPI.Controllers
{
    public class ProdutoController : ApiController
    {
        private DbVendasDataContext contexto = new DbVendasDataContext();

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Json(contexto.Produtos.ToList());
        }

        [HttpGet]
        public IHttpActionResult Get(int produtoId)
        {
            return Json(contexto.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId));
        }

        [HttpPost]
        public IHttpActionResult Post(Produto produto)
        {
            contexto.Produtos.InsertOnSubmit(produto);
            contexto.SubmitChanges();
            return Json(produto);
        }

        [HttpPut]
        public IHttpActionResult Put(int produtoId, Produto produto)
        {
            Produto temp = contexto.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

            temp.Descricao = produto.Descricao;
            temp.FabricanteId = produto.FabricanteId;
            temp.QtdEstoque = produto.QtdEstoque;
            temp.Preco = produto.Preco;

            contexto.SubmitChanges();

            return Json(temp);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int produtoId)
        {
            Produto temp = contexto.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

            contexto.Produtos.DeleteOnSubmit(temp);

            contexto.SubmitChanges();

            return Json(temp);
        }
    }
}

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
        public List<Produto> Get()
        {
            return contexto.Produtos.ToList();
        }

        [HttpGet]
        public Produto Get(int produtoId)
        {
            return contexto.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);
        }

        [HttpPost]
        public Produto Post(Produto produto)
        {
            contexto.Produtos.InsertOnSubmit(produto);
            contexto.SubmitChanges();
            return produto;
        }

        [HttpPut]
        public Produto Put(int produtoId, Produto produto)
        {
            Produto temp = contexto.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

            temp.Descricao = produto.Descricao;
            temp.FabricanteId = produto.FabricanteId;
            temp.QtdEstoque = produto.QtdEstoque;
            temp.Preco = produto.Preco;

            contexto.SubmitChanges();

            return temp;
        }

        [HttpDelete]
        public Produto Delete(int produtoId)
        {
            Produto temp = contexto.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);

            contexto.Produtos.DeleteOnSubmit(temp);

            contexto.SubmitChanges();

            return temp;
        }
    }
}

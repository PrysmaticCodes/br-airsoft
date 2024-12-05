using Microsoft.AspNetCore.Mvc;
using TCC_BR.Data;
using TCC_BR.Models;
using TCC_BR.Repository;
using TCC_BR.Repository.Contract;

namespace TCC_BR.Controllers
{
    public class ProdutoController : Controller
    {
        private IProdutoRepository _produtoRepository;
        public ProdutoController(IProdutoRepository ProdutoRepository)
        {

            _produtoRepository = ProdutoRepository;
        }

        //Cadastra Livro Get
        public IActionResult Index()
        {
            return View(_produtoRepository.ObtertodosProdutos());
        }
        //Cadastra Livro Post
        [HttpPost]
        public IActionResult Index(Produto produto, IFormFile file)
        {
            var Caminho = GerenciaArquivo.CadastrarImagemProduto(file);

            produto.ImagemProd = Caminho;
            _produtoRepository.Cadastrar(produto);

            ViewBag.msg = "Cadastro realizado com sucesso";

            return View();
        }

        public IActionResult CadProduto()
        {
            return View();
        }
        //Cadastra Produto Post
        [HttpPost]
        public IActionResult CadProduto(Produto produto, IFormFile file)
        {
            var Caminho = GerenciaArquivo.CadastrarImagemProduto(file);

            produto.ImagemProd = Caminho;
            _produtoRepository.Cadastrar(produto);

            ViewBag.msg = "Cadastro realizado com sucesso";

            return View();
        }

    }
}

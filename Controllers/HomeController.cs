using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TCC_BR.Repository.Contract;
using TCC_BR.Libraries.CarrinhoCompra;
using TCC_BR.Models;
using TCC_BR.Repository;
using TCC_BR.Libraries.Login;
using TCC_BR.Libraries.Filtro;

namespace TCC_BR.Controllers
{
    public class HomeController : Controller
    {
        private IProdutoRepository _produtoRepository;
        private CookieCarrinhoCompra _cookieCarrinhoCompra;
        private ICompraRepository _compraRepository;
        private LoginUsuario _LoginUsuario;

        public HomeController(IProdutoRepository produtoRepository, CookieCarrinhoCompra cookieCarrinhoCompra, ICompraRepository compraRepository, LoginUsuario usuarioLogin)
        {
            
            _cookieCarrinhoCompra = cookieCarrinhoCompra;
            _produtoRepository = produtoRepository;
            _compraRepository = compraRepository;
            _LoginUsuario = usuarioLogin;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AdicionarItem(int id)
        {
            Produto produto = _produtoRepository.ObtertoProduto(id);

            if (produto == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var item = new Produto()
                {
                    ID = id,
                    QuantProd = 1,
                    ImagemProd = produto.ImagemProd,
                    NomeProduto = produto.NomeProduto
                };
                _cookieCarrinhoCompra.Cadastrar(item);

                return RedirectToAction(nameof(Carrinho));
            }


        }

        public IActionResult Carrinho()
        {
            return View(_cookieCarrinhoCompra.Consultar());
        }

        public IActionResult RemoverProduto(int id)
        {
            _cookieCarrinhoCompra.Remover(new Produto() { ID = id });
            return RedirectToAction(nameof(Carrinho));
        }

        DateTime data;
        [UsuarioAutorizacao]
        public IActionResult SalvarCarrinho(Compra compra)
        {
            List<Produto> carrinho = _cookieCarrinhoCompra.Consultar();

            Compra mdC = new Compra();
            Produto mdP = new Produto();

            data = DateTime.Now.ToLocalTime();

            mdC.DataCompra = data.ToString("dd/MM/yyyy");
            mdC.RefUsuario.Id = Convert.ToInt32(_LoginUsuario.GetUsuario().Id);
            _compraRepository.Cadastrar(mdC);

            _compraRepository.buscaIdCompra(compra);

            for (int i = 0; i < carrinho.Count; i++)
            {

                mdC.CodCompra = Convert.ToString(compra.CodCompra);
                mdP.ID = Convert.ToInt32(carrinho[i].ID);

                _produtoRepository.Cadastrar(mdP);
            }

            _cookieCarrinhoCompra.RemoverTodos();
            return RedirectToAction("confEmp");
        }
    }
}

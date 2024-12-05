using Newtonsoft.Json;
using TCC_BR.Models;

namespace TCC_BR.Libraries.CarrinhoCompra
{
    public class CookieCarrinhoCompra
    {
        //criar uma chave
        private string Key = "Carrinho.Compras";
        private Cookie.Cookie _cookie;

        public CookieCarrinhoCompra(Cookie.Cookie cookie)
        {
            _cookie = cookie;
        }
        /*
         * CRUD - Cadastrar, Read, Update, Delete
         * Adicionar Item, Remover Item, Alterar Quantidade
         */

        //Salvar
        public void Salvar(List<Produto> Lista)
        {
            string Valor = JsonConvert.SerializeObject(Lista);
            _cookie.Cadastrar(Key, Valor);
        }
        //Consulta
        public List<Produto> Consultar()
        {
            if (_cookie.Existe(Key))
            {
                string valor = _cookie.Consultar(Key);
                return JsonConvert.DeserializeObject<List<Produto>>(valor);
            }
            else
            {
                return new List<Produto>();
            }
        }
        //Cadastrar
        public void Cadastrar(Produto item)
        {
            List<Produto> Lista;
            if (_cookie.Existe(Key))
            {
                Lista = Consultar();
                var ItemLocalizado = Lista.SingleOrDefault(a => a.ID == item.ID);

                if (ItemLocalizado == null)
                {
                    Lista.Add(item);
                }
                else
                {
                    ItemLocalizado.QuantProd = ItemLocalizado.QuantProd + 1;

                }
            }
            else
            {
                Lista = new List<Produto>();
                Lista.Add(item);
            }
            // Criar o metrodo salvar
            Salvar(Lista);
        }
        //Atualiza 
        public void Atualizar(Produto item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.ID == item.ID);

            if (ItemLocalizado != null)
            {
                ItemLocalizado.QuantProd = item.QuantProd + 1;
                Salvar(Lista);
            }
        }
        // remove item
        public void Remover(Produto item)
        {
            var Lista = Consultar();
            var ItemLocalizado = Lista.SingleOrDefault(a => a.ID == item.ID);

            if (ItemLocalizado != null)
            {
                Lista.Remove(ItemLocalizado);
                Salvar(Lista);
            }
        }
        // Verifica se existe
        public bool Existe(string Key)
        {
            if (_cookie.Existe(Key))
            {
                return false;
            }

            return true;
        }
        // Remove todos itens do carrinho
        public void RemoverTodos()
        {
            _cookie.Remover(Key);
        }

    }
}

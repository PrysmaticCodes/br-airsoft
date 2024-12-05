namespace TCC_BR.Models
{
    public class Compra
    {
        public string CodCompra { get; set; }

        public string DataCompra { get; set; }

        public Usuario RefUsuario { get; set; }

        // public Produto RefProduto { get; set; }

    }
}

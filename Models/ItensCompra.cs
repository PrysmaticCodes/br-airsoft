namespace TCC_BR.Models
{
    public class ItensCompra
    {
        public int IdItensCompra { get; set; }

        public Compra RefCompra { get; set; }

        public Produto RefProduto { get; set; }

        public decimal ValorTotal { get; set; }
    }
}

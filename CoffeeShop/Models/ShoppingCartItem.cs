namespace CoffeeShop.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }

        public Product? Property { get; set; } 

        public int Qty { get; set; }

        public string? ShoppingCartID { get; set; }
    }
}

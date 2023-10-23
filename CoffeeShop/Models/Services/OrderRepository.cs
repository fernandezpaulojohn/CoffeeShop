using CoffeeShop.Data;
using CoffeeShop.Models.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.Models.Services
{
    public class OrderRepository : IOrderRepository
    {
        private CoffeeShopDbContext dbContext;
        private IShoppingCartRepository shoppingCartrepository;
        
        public OrderRepository(CoffeeShopDbContext dbContext, IShoppingCartRepository shoppingCartRepository)
        {
            this.dbContext = dbContext;
            this.shoppingCartrepository = shoppingCartRepository;
        }
        public void PlaceOrder(Order order)
        {
           var shoppingCartItems = shoppingCartrepository.GetShoppingCartItems();
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = item.Qty,
                    ProductId = item.Product.Id,
                    Price = item.Product.Price
                };
                order.OrderDetails.Add(orderDetail);
            }
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = shoppingCartrepository.GetShoppingCartTotal();
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
        }
    }
}

using CoffeeShop.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        
            private IShoppingCartRepository shoppingCartRepository;
            private IProductRepository productRepository;

            public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
            {
                this.shoppingCartRepository = shoppingCartRepository;
                this.productRepository = productRepository;
            }

            public IActionResult Index()
            {
                var items = shoppingCartRepository.GetShoppingCartItems();
                shoppingCartRepository.ShoppingCartItems = items;
                shoppingCartRepository.GetShoppingCartTotal();
                 return View(items);
            }

            public RedirectToActionResult AddtoShoppingCart(int pId)
            {
              var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id == pId);
                if (product != null)
                {
                    shoppingCartRepository.AddToCart(product);
                }
                return RedirectToAction("Index");
            }

            public RedirectToActionResult RemoveFromShoppingCart(int pId)
            {
                var product = productRepository.GetAllProducts().FirstOrDefault(p => p.Id == pId);
                if (product != null)
                {
                    shoppingCartRepository.RemoveFromCart(product);
                }
                return RedirectToAction("Index");
            }
    }
}

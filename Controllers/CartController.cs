using BooksStore.Data;
using BooksStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public static Cart? _cart;
        public CartController(ApplicationDbContext db, Cart cart)
        {
            _db = db;
            _cart = cart;
        }

        public IActionResult Index()
        {
            _cart.ListCartItem = _cart.GetCartItems();

            return View(_cart);
        }

        public ActionResult AddToCart(int Id)
        {
            Book? item = _db.Books.FirstOrDefault(i => i.Id == Id);
            if (item != null)
            {
                _cart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int Id)
        {
            CartItem? item = _db.CartItem.FirstOrDefault(i => i.Id == Id);
            if (item != null)
            {
                _cart.RemoveFromCart(item);
            }

            TempData["message"] = string.Format("The book has been removed!");
            return RedirectToAction("Index");
        }
    }
}

using BooksStore.Models;
using Microsoft.AspNetCore.Mvc;
using BooksStore.Data;
using Models;

namespace BooksStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        //[HttpPost]
        public IActionResult Checkout(Order order)
        {

            if (CartController._cart.ListCartItem.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                int counter = 0;
                int[] array = new int[CartController._cart.ListCartItem.Count];
                foreach (var item in CartController._cart.ListCartItem)
                {
                    array[counter] = (int)item.book.Id;
                    counter++;
                }
                order.booksList = String.Join(",", array);

                _db.Orders.Add(order);
                _db.SaveChanges();
                CartController._cart.Clear();
                return RedirectToAction("Complete");
            }
            else
            {
                return View(order);
            }
        }
        public IActionResult Complete()
        {
            return View();
        }
    }
}

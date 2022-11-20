using BooksStore.Data;
using BooksStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using NuGet.Protocol.Core.Types;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;

namespace BooksStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var BooksList = _db.Books.ToList();
            return View(BooksList);
        }

        public IActionResult Edit(int bookId)
        {
            Book? book = _db.Books.FirstOrDefault(g => g.Id == bookId);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.Id == null)
                    _db.Books.Add(book);
                else
                {
                    Book? dbEntry = _db.Books.Find(book.Id);
                    if (dbEntry != null)
                    {
                        dbEntry.Name = book.Name;
                        dbEntry.Price = book.Price;
                        dbEntry.Category = book.Category;
                    }
                }
                _db.SaveChanges();
                TempData["message"] = string.Format($"Changes for {book.Name} have been saved!");
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
            
        }
        public IActionResult Create()
        {
            return View("Edit", new Book());
        }
        public IActionResult DeleteBook(int Id)
        {
            Book? book = _db.Books.Where(o => o.Id == Id).FirstOrDefault();
            if (book!=null)
            {
                _db.Books.Remove(book);
            }
            _db.SaveChanges();

            TempData["message"] = string.Format("The book has been removed!");
            return RedirectToAction("Index");
        }
        public IActionResult ListOfOrders()
        {
            var listOfOrders = _db.Orders.ToList();
            return View(listOfOrders);
        }

        public IActionResult DeleteOrder(int OrderID)
        {
            Order? order = _db.Orders.Where(o => o.OrderID == OrderID).FirstOrDefault();
            if (order!=null)
            {
                _db.Orders.Remove(order);
            }
            _db.SaveChanges();

            TempData["message"] = string.Format("The Order has been removed!");
            return RedirectToAction("ListOfOrders");
        }
        public IActionResult OrderDetails(int OrderID)
        {
            Order? order = _db.Orders.Where(o => o.OrderID == OrderID).FirstOrDefault();
            List<Book>? AllBooks = _db.Books.ToList();
            List<Book>? BooksInOrder= new List<Book>(); ;
            string[] asd = order.booksList.Split(',');
            int[] booksId = new int[asd.Length];
            int counter=0;
            foreach (var sub in asd)
            {
                if (Int32.TryParse(sub, out int numValue))
                {
                    booksId[counter]=numValue;
                    counter++;
                }
                else
                {
                    continue;
                }
            }

            foreach (var item in booksId)
            {
                try
                {
                    Book? book = AllBooks.Where(o => o.Id == item).FirstOrDefault();
                    if (order != null)
                    {
                        BooksInOrder.Add(book);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                
            }

            return View(BooksInOrder);
        }
    }
}

using BooksStore.Data;
using Microsoft.EntityFrameworkCore;

namespace BooksStore.Models
{
   
        public class Cart
        {
        private readonly ApplicationDbContext _db;
        public Cart(ApplicationDbContext db)
        {
            _db = db;
        }
        public string? CartItemId { get; set; }
        public  List<CartItem> ListCartItem { get; set; }=new List<CartItem>();
        public static Cart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<ApplicationDbContext>();
            string bookId = session.GetString("Id")??Guid.NewGuid().ToString();
            session.SetString("Id", bookId);
            return new Cart(context) 
            {
                CartItemId = bookId 
            };
        }
        public void AddToCart(Book book)
        {
            _db.CartItem.Add(new CartItem
            {
                book = book,
                Price = book.Price,
                CartItemId = CartItemId
            });
            _db.SaveChanges();
        }
        public void RemoveFromCart(CartItem item)
        {
            _db.CartItem.Remove(item);
            _db.SaveChanges();
        }
        public List<CartItem> GetCartItems()
        {
            return _db.CartItem.Where(c => c.CartItemId == CartItemId).Include(s => s.book).ToList();
        }
        public virtual void Clear() => ListCartItem.Clear(); 
    }
}

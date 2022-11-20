namespace BooksStore.Models
{
    public class CartItem
    {
        public int? Id { get; set; }
        public decimal Price { get; set; }
        public Book book { get; set; }
        public string CartItemId { get; set; }

    }
}

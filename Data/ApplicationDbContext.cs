using Microsoft.EntityFrameworkCore;
using BooksStore.Models;
using Models;

namespace BooksStore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {

        }
        public DbSet<Book> Books  => Set<Book>();
        public DbSet<CartItem> CartItem => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();

        
    }
}

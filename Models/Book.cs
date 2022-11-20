using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksStore.Models
{
    public class Book
    {
        [HiddenInput(DisplayValue = false)]
        public  int? Id { get; set; } 
        [Required]
        public string? Name { get; set; }
        //public string Description { get; set; }
        public string? Category { get; set; }
        [Column(TypeName = "decimal(8, 2)")]

        public decimal Price { get; set; }
    }
}

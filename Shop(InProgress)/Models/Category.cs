using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę kategorii")]
        [StringLength(100)]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Wprowadź opis kategorii")]
        public string CategoryDescription { get; set; }
        public string IconFileName { get; set; }

        public virtual ICollection<Course> Course { get; set; }
    }
}
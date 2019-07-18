using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Wprowadź nazwę kursu")]
        [StringLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwę autora")]
        [StringLength(30)]
        public string Authors { get; set; }
        public DateTime AddDate { get; set; }
        [StringLength(30)]
        public string PictureFileName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool Bestseller { get; set; }
        public bool Hidden { get; set; }
        public string ShortDescription { get; set; }

        public virtual Category Category { get; set; }
    }
}
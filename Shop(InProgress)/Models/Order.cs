using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Wprowadź imię")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Wprowadź nazwisko")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Wprowadź ulicę")]
        [StringLength(100)]
        public string Street { get; set; }
        [Required(ErrorMessage = "Wprowadź miasto")]
        [StringLength(100)]
        public string City { get; set; }
        [Required(ErrorMessage = "Wprowadź kod pocztowy")]
        [StringLength(6)]
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public DateTime AddDate { get; set; }
        public OrderState OrderState { get; set; }
        public decimal OrderPrice { get; set; }

        List<OrderPosition> OrderPosition { get; set; }
    }

    public enum OrderState
    {
        New,
        Completed
    }
}
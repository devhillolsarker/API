using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class MvcCustomerModel
    {
        public int CustomerID { get; set; }

        public string CustomerName { get; set; }
        [Required(ErrorMessage ="Customer Name is required")]
        public string SendArea { get; set; }
        public int PostalCode { get; set; }
    }
}
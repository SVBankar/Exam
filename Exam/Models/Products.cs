using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exam.Models
{
    public class Products
    {
        [key]
        public int ProductId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage ="Plese Enter Product name")]
        public string ProductName { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage ="Enter Rate")]
        public long Rate { get; set; }


        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Plese Describe the product")]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Plese mention Category")]
        public string CategoryName { get; set; }


    }
}
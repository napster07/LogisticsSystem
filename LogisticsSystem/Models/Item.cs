using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LogisticsSystem.Models
{
    public class Item
    {
        [Display(Name = "Item ID")]
        public int ItemID { get; set; }
        [Required]
        [Display(Name = "Item Name")]
        public string ItemName { get; set; }
        [Required]
        [Display(Name = "Weight(kg)")]
        public decimal Weight { get; set; }
        public decimal Cost { get; set; }
        public int Priority { get; set; }
        public string Type { get; set; }
    }
}
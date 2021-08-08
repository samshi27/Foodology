using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDatabase
{
    public class Cart
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public int itemQty { get; set; }
        public int itemPrice { get; set; }
        public int itemBill { get; set; }
    }
}

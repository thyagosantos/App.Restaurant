using Orders.API.Models.Enum;
using System.Collections.Generic;

namespace Orders.API.Models
{
    public class Order
    {
        public IEnumerable<Dish> Dishes { get;  set; }
        public PeriodOfDay Period { get;  set; }       
             
    }
}

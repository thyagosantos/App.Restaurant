using Orders.API.Models;
using Orders.API.Models.Enum;
using System.Collections.Generic;

namespace Orders.API.Colections
{
    public struct  ListOfDishes
    {             
        public static readonly IEnumerable<Dish> MorningDishe = new List<Dish>()
        {
            new Dish((DishType)1, "eggs"),
            new Dish((DishType)2, "toast"),
            new Dish((DishType)3, "coffee",int.MaxValue),
            new Dish((DishType)4, "error")
        };

        public static readonly IEnumerable<Dish> NightDisheTypes = new List<Dish>()
        {
            new Dish((DishType)1, "steak"),
            new Dish((DishType)2, "potato",int.MaxValue),
            new Dish((DishType)3, "wine"),
            new Dish((DishType)4, "cake")           
        };
    }
}

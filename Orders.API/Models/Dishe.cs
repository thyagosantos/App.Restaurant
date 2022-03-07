using Orders.API.Models.Enum;

namespace Orders.API.Models
{
    public class Dish
    {       
        public DishType Type { get; set; }

        public string Name { get; set; }

        public int QtyAllowed { get; set; }

        public Dish(DishType _type,string _name , int _qtyAllowed = 1)
        {
            Name = _name;
            Type = _type;
            QtyAllowed = _qtyAllowed;
        }
    } 
}

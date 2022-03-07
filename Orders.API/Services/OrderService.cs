using Orders.API.Colections;
using Orders.API.Models;
using Orders.API.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orders.API.Utils;

namespace Orders.API.Services
{
    public class OrderService : IDisposable
    {
        public string ValidateOrder(string _filter)
        {
            if (string.IsNullOrEmpty(_filter))
                return "Invalid Order, Blank Order";

            var specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_";
            if (StringExtension.IsMatch(_filter, specialChar))
                return "Invalid Order, special characters are not allowed";

            var OrderList = _filter.Trim().Split(',').ToList();

            var Blank = OrderList.Find((x => string.IsNullOrEmpty(x)));
            if (Blank != null)
                return "Invalid Order, Expected Values After a Comma";
            var notAllowed = "1234567890,";
            if (StringExtension.IsMatch(OrderList[0], notAllowed))
                return "Invalid Order, First Expected Value:, Time of Day";

            if (!System.Enum.TryParse(OrderList[0].ToLower(), out PeriodOfDay period))
            {
                return "Invalid Order, Available Service Period: Morning and Night";
            }           

            if (OrderList.Count < 2)
                return "Invalid Order, No Dishes";

            return "";
        }

        public string GetOrder(string _filter)
        {
            var OrderList = ReadOrders(_filter);

            return PrintOrderDishes(OrderList);

        }

        private string PrintOrderDishes(Order _order)
        {
            //remove duplicates and add counter
            var query = _order.Dishes.GroupBy(x => x)           
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();

            StringBuilder builder = new();
            string delimiter = "";
            foreach (var item in query)
            {
                builder.Append(delimiter);
                builder.Append(item.Element.Name);
                if (item.Counter > 1 )
                {
                    builder.Append("(x"+item.Counter+")" );
                }
                delimiter = ", ";

            }
            return builder.ToString();
        }

        private Order ReadOrders(string _filter)
        {
            var OrderDishKeys = _filter.Trim().Split(',').ToList();

            var order = new Order();
            if (Enum.TryParse(OrderDishKeys[0].ToLower(), out PeriodOfDay period))
            {
                order.Period = period;

                OrderDishKeys.RemoveAt(0);

                if (order.Period == PeriodOfDay.morning)
                    order.Dishes = AddDishesToOrder(ListOfDishes.MorningDishe, OrderDishKeys);

                if (order.Period == PeriodOfDay.night)
                    order.Dishes = AddDishesToOrder(ListOfDishes.NightDisheTypes, OrderDishKeys);
            }
            return order;
        }        

        private  IEnumerable<Dish> AddDishesToOrder(IEnumerable<Dish> _listDishAvailable,
                                                           IEnumerable<string> _orderDishesKeys)
        {
            var ordem = new List<Dish>();

            foreach (var item in _orderDishesKeys)
            {
                // set the first index in the list to 1
                var dishKey = Convert.ToInt32(item) - 1;
               
                if ( dishKey >= _listDishAvailable.Count() )
                {
                    ordem.Add(new Dish(DishType.error, "error"));
                    break;
                }

                if (ordem.Count == 0)
                {
                    ordem.Add(_listDishAvailable.ElementAt(dishKey));
                    continue;
                }

                if (!ordem.Contains(_listDishAvailable.ElementAt(dishKey)))
                {
                    ordem.Add(_listDishAvailable.ElementAt(dishKey));
                }
                else               
                {
                    var count = ordem.Count(x => x == _listDishAvailable.ElementAt(dishKey));

                    if (count < _listDishAvailable.ElementAt(dishKey).QtyAllowed)
                    { 
                        ordem.Add(_listDishAvailable.ElementAt(dishKey)); 
                    }
                    else
                    {
                        ordem.Add(new Dish(DishType.error, "error"));
                        break;
                    }
                }
            }

            return ordem.OrderBy(x => (int)(x.Type));
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.Dispose();
        }
    }
}

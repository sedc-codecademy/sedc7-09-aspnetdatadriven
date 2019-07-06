using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.G2.Dapper.ConsoleApp
{
    public class PizzaSize
    {
        public int PizzaSizeId { get; set; }
        public int PizzaId { get; set; }
        public int SizeId { get; set; }
        public decimal Price { get; set; }

        public virtual Pizza Pizza { get; set; }
    }
}

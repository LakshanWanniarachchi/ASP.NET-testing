using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class StockDto
    {

        public int id { get; set; }

        public string Symbol { get; set; }


        public string CompanyName { get; set; }


        public decimal Purchase { get; set; }



        public decimal LastDiv { get; set; }

        public String Industry { get; set; }

        public long MarketCap { get; set; }


    }
}
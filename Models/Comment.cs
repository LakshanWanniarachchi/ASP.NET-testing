using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {

        [Key]
        public int Id { get; set; }


        public String Title { get; set; } = string.Empty;

        public String Content { get; set; } = string.Empty;

        public DateTime Created { get; set; } = DateTime.Now;

        public int? StockId { get; set; }

        public Stock Stock { get; set; }





    }
}
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertigonTask_API_V3.Entities
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public DateTime created_time { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MultiMap.Data.Models
{
    public class Arealtype
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public string UserID { get; set; }
    }
}

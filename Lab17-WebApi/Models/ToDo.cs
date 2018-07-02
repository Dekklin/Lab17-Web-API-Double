using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab17_WebApi
{
    public class ToDo
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public bool Finished { get; set; }
        public int ListId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab17_WebApi.Data
{
    public class ToDoList
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<ToDo> Contents { get; set; }
    }
}

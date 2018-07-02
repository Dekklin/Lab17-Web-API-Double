using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab17_WebApi.Data
{
    public class ToDoViewModel
    {
        public IEnumerable<ToDo> ToDos { get; set; }
    }
}

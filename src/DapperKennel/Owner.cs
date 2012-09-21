using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DapperKennel
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Dog> Dogs { get; set; }
    }
}

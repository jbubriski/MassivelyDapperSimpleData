using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperKennel
{
    public class Dog
    {
        public int Id { get; set; }

        public Owner Owner { get; set; }
        public Breed Breed { get; set; }

        public string Name { get; set; }

        public int Legs { get; set; }

        public bool HasTail { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Massive;

namespace MassiveKennel
{
    public class Dogs : DynamicModel
    {
        // yeah this is really all the code we need to write
        public Dogs() : base ("Massive") { }
    }
}

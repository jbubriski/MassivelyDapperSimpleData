using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicKennel
{
    class Program
    {
        static void Main( string[] args )
        {
            dynamic exampleSimpleDynamic = new SimpleDynamic();

            Console.WriteLine( exampleSimpleDynamic.super_method_is_super() );

            Console.ReadLine();
        }
    }

    public class SimpleDynamic : DynamicObject
    {
        public override bool TryInvokeMember( InvokeMemberBinder binder, object[] args, out object result )
        {
            if ( binder.Name.ToLower().StartsWith( "super" ) )
            {
                result = "The greatest thing in the world!";
                return true; // tell runtime we have handled "super*" method!
            }
            return base.TryInvokeMember( binder, args, out result );
        }
    }
}

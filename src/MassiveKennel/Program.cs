using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassiveKennel
{
    class Program
    {
        static void Main(string[] args)
        {
            var dogsTable = new Dogs();

            var allDogs = dogsTable.All();

            foreach( var dog in allDogs )
            {
                Console.WriteLine( dog.Name );
            }


            var allDogsAndOwners = dogsTable.Query(
                                    @"
                                        SELECT 
                                            D.Id, D.Name, D.Legs, D.HasTail,
                                            O.Id as [OwnerId], O.Name as [OwnerName],
                                            B.Name as [Breed]
                                        FROM dogs D
                                        INNER JOIN owners O ON o.id = d.ownerid
                                        INNER JOIN breeds B ON b.id = d.breedid
                                    " );

            foreach( var dog in allDogsAndOwners )
            {
                Console.WriteLine( "{0}\t{1}\t{2}", dog.Name, dog.OwnerName, dog.Breed );
            }


            Console.ReadLine();
        }
    }
}

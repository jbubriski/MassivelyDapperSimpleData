using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simple.Data;

namespace SimpleDataKennel
{
    class Program
    {
        static void Main(string[] args)
        {
            // get all the dogs
            var db = Database.Open();

            var dogs = db.Dogs.All();
            foreach( var dog in dogs )
            {
                Console.WriteLine( dog.name );
            }


            // get all the dogs, their owners name, and the dogs breed
            var dogsAndOwners = db.Dogs.Query()
                                    .Join( db.Owners ).On( db.Owners.Id == db.Dogs.OwnerId )
                                    .Join( db.Breeds ).On( db.Breeds.Id == db.Dogs.BreedId )
                                    .Select( db.Dogs.Name, db.Owners.Name.As( "OwnerName" ), db.Breeds.Name.As("Breed") );

            foreach( var dog in dogsAndOwners )
            {
                Console.WriteLine( "{0}\t{1}\t{2}", dog.Name, dog.OwnerName, dog.Breed );
            }

            Console.ReadLine();
        }
    }
}

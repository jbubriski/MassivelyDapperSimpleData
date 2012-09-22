using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Kennel.Helpers;

namespace DapperKennel
{
    class Program
    {
        static void Main(string[] args)
        {
            ListOwners();
            ListOwnersDetailed();
            ListDogs();
            ListDogsDetailed();

            Console.ReadKey();
        }

        private static void ListOwners()
        {
            var owners = GetOwners();

            foreach (var owner in owners)
            {
                Console.WriteLine(owner.Name);
            }
        }

        private static IEnumerable<Owner> GetOwners()
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                return connection.Query<Owner>(@"
SELECT *
FROM Owners");
            }
        }

        private static void ListOwnersDetailed()
        {
            var owners = GetOwnersDetailed();

            foreach (var owner in owners)
            {
                Console.WriteLine(owner.Name);

                foreach (var dog in owner.Dogs)
                {
                    Console.WriteLine("\t{0}", dog.Name);
                }
            }
        }

        private static IEnumerable<Owner> GetOwnersDetailed()
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                var owners = new Dictionary<int, Owner>();

                // Query<Parent, Child, Parent>(sql, MapFunction)
                connection.Query<Owner, Dog, Owner>(@"
SELECT *
FROM Owners o
JOIN Dogs d on o.Id = d.OwnerId",
                    (owner, dog) => {
                        Owner found;

                        if (!owners.TryGetValue(owner.Id, out found))
                        {
                            owners.Add(owner.Id, found = owner);
                        }
                        
                        found.Dogs.Add(dog);

                        return found;    
                    });

                return owners.Values;
            }
        }

        private static void ListDogs()
        {
            var dogs = GetDogs();

            foreach (var dog in dogs)
            {
                Console.WriteLine(dog.Name);
            }
        }

        private static IEnumerable<Dog> GetDogs()
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                // Query<Parent, Child, Parent>(sql, MapFunction)
                return connection.Query<Dog>(@"
SELECT *
FROM Dogs d");
            }
        }

        private static void ListDogsDetailed()
        {
            var dogs = GetDogsDetailed();

            foreach (var dog in dogs)
            {
                Console.WriteLine(dog.Name);
                Console.WriteLine("\t{0}", dog.Owner.Name);
            }
        }

        private static IEnumerable<Dog> GetDogsDetailed()
        {
            using (var connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                // Query<Parent, Child, Parent>(sql, MapFunction)
                return connection.Query<Dog, Owner, Dog>(@"
SELECT *
FROM Dogs d
JOIN Owners o on o.Id = d.OwnerId",
                    (dog, owner) =>
                    {
                        dog.Owner = owner;
                        return dog;
                    });
            }
        }
    }
}

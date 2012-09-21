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
            Setup();

            ConsoleKeyInfo key;

            do
            {
                PrintMenu();

                key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case '1':
                        ListOwners();
                        break;
                    case '2':
                        ListOwnersDetailed();
                        break;
                    case '3':
                        ListDogs();
                        break;
                    case '4':
                        ListDogsDetailed();
                        break;
                    case '5':
                        Console.WriteLine("Say What?");
                        break;
                }
            }
            while (key.Key != ConsoleKey.Escape);
        }

        private static void Setup()
        {
            var createSql = @"
                create table #Owners (Id int, Name varchar(20))
                create table #Dogs (Id int, OwnerId int, Name varchar(20))

                insert #Owners values(1, 'John')
                insert #Owners values(2, 'Jared')

                insert #Dogs values(1, 1, 'Skippy')
                insert #Dogs values(2, 1, 'Waffles')
                insert #Dogs values(3, 2, 'Bizkit')
                insert #Dogs values(4, 3, 'Gregg')
";
            using (var connection = ConnectionHelper.GetConnection())
            {
                connection.Open();

                connection.Execute(createSql);
            }
        }

        private static void PrintMenu()
        {
            Console.Clear();

            Console.WriteLine("Kennel Main Menu");

            Console.WriteLine("1. List Owners");
            Console.WriteLine("2. List Owners Detailed");
            Console.WriteLine("3. List Dogs");
            Console.WriteLine("4. List Dogs Detailed");
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

                // Query<Parent, Child, Parent>(sql, MapFunction)
                return connection.Query<Owner, Dog, Owner>(@"
SELECT *
FROM Owners o
JOIN Dogs d on o.Id = d.OwnerId", 
                    (owner, dog) => { owner.Dogs.Add(dog); return owner; });
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
            var dogs = GetDogs();

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

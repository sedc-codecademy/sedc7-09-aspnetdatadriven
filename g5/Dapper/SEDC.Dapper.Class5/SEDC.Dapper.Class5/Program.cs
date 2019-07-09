using SEDC.Dapper.Class5.DataAccess;
using SEDC.Dapper.Class5.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.Dapper.Class5
{
    class Program
    {
        private static readonly string _connectionString = "Server=.;Database=BooksDB2019;Trusted_Connection=True;";
        private static IUnitOfWork _uow = new UnitOfWork(_connectionString);
 
        static void Main(string[] args)
        {
            Console.WriteLine("Select Option:");
            Console.WriteLine("1) All Authors");
            Console.WriteLine("2) All Novels");
            Console.WriteLine("3) All Novels With Authors");
            string option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    Console.WriteLine("Enter Name Fragment:");
                    string fragment = Console.ReadLine();
                    List<Author> authors = _uow.AuthorsRepository.GetAll(fragment).ToList();
                    foreach (Author author in authors)
                    {
                        Console.WriteLine($"{author.Name} - {author.DateOfBirth}");
                    }
                    break;
                case "2":
                    List<Novel> novels = _uow.NovelsRepository.GetAll().ToList();
                    foreach (Novel novel in novels)
                    {
                        Console.WriteLine(novel.ToString());
                    }
                    break;
                case "3":
                    List<Author> authorsWithNovels = _uow.AuthorsRepository.GetAllWithNovels().ToList();
                    foreach (Author author in authorsWithNovels)
                    {
                        Console.WriteLine(author.ToString());
                    }
                    break;
                default:
                    Console.WriteLine("Wrong Option!");
                    break;
            }
            Console.ReadLine();
        }
    }
}

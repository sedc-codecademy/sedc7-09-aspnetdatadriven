using SEDC.Entity.Class3.DataAccess.UOW;
using SEDC.Entity.Class3.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEDC.Entity.Class3
{
    class Program
    {
        // Scaffold-DbContext "Server=.;Database=BooksDB2019;Trusted_Connection=True;"
        // Microsoft.EntityFrameworkCore.SqlServer -OutputDir Domain
        static void Main(string[] args)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1) Authors");
            Console.WriteLine("2) Novels");
            Console.WriteLine("3) Authors And Novels");
            Console.WriteLine("4) All");
            int result = int.Parse(Console.ReadLine());

            using(UnitOfWork uow = new UnitOfWork())
            {
                try
                {
                    if (result == 1)
                    {
                        List<Authors> authors = uow.AuthorsRepository.GetAll();
                        Console.WriteLine("Authors:");
                        foreach (var author in authors)
                        {
                            Console.WriteLine($"{author.Id}) {author.Name} Born: {author.DateOfBirth}");
                        }
                    }
                    else if (result == 2)
                    {
                        List<Novels> novels = uow.NovelsRepository.GetAll();
                        Console.WriteLine("Novels:");
                        foreach (var novel in novels)
                        {
                            Console.WriteLine($"{novel.Id}) {novel.Title} Is Read: {novel.IsRead}");
                        }
                    }
                    else if (result == 3)
                    {
                        List<Authors> authors = uow.AuthorsRepository.GetAll();
                        List<Novels> novels = uow.NovelsRepository.GetAll();
                        Console.WriteLine("Authors:");
                        foreach (var author in authors)
                        {
                            Console.WriteLine($"{author.Id}) {author.Name} Born: {author.DateOfBirth}");
                        }
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("Novels:");
                        foreach (var novel in novels)
                        {
                            Console.WriteLine($"{novel.Id}) {novel.Title} Is Read: {novel.IsRead}");
                        }
                    }
                    else
                    {
                        List<Authors> all = uow.AuthorsRepository.GetAllWithMembers();
                        Console.WriteLine("Everything");
                        foreach (var author in all)
                        {
                            Console.WriteLine($"{author.Id}) {author.Name} Born: {author.DateOfBirth}, Books: {author.Novels.Count}");
                            Novels firstBook = author.Novels != null && author.Novels.Count != 0 ?
                                author.Novels.First() : null;
                            if (firstBook == null)
                            {
                                Console.WriteLine("No books!");
                            }
                            else
                            {
                                string award = firstBook.Nominations != null && firstBook.Nominations.Count != 0 ?
                                    firstBook.Nominations.First().Award.AwardName : "No nominations";
                                Console.WriteLine($"    First Book: {firstBook.Title}, Nomination: {award}");
                            }
                        }

                    }
                    uow.Commit(); // Here we save all changes to the database ( In this example there are no changes in the database )
                }
                catch (Exception ex)
                {
                    uow.Reject();
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
        }
    }
}

using System;
using System.Security.Cryptography.X509Certificates;
using Autofac;

namespace AnagramChecker
{    
    static class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
           AnagramCheck();
           CountLines(args[0]);           
        }

        private static void AnagramCheck()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AnagramChecker>().As<IAnagramChecker>();
            Container = builder.Build();
            using (var scope = Container.BeginLifetimeScope())
            {
                var anagramChecker = scope.Resolve<IAnagramChecker>();
                Console.WriteLine("Enter two strings for Anagram check !");
                var string1 = Console.ReadLine();
                var string2 = Console.ReadLine();
                if (anagramChecker.IsAnagram(string1, string2))
                {
                    Console.WriteLine("The strings are anagrams !");
                }
                else
                {
                    Console.WriteLine("The strings are not anagrams !");
                }
            }
        }

        private static void CountLines(string filePath)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LocCounter>().As<ILocCounter>();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var locCounter = scope.Resolve<ILocCounter>();
                var noOfLines = locCounter.CountLines(Helper.ReadFile(filePath));
                Console.WriteLine("The Number of lines in " + filePath + ":" + noOfLines);
            }
        }
    }
}

using Autofac;
using AnagramChecker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemTest
{
    [TestClass]
    public class AnagramTest
    {
        private static IContainer Container { get; set; }
        [TestMethod]
        public void Test_Anagram_Checker_All_Cases()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AnagramChecker.AnagramChecker>().As<IAnagramChecker>();
            Container = builder.Build();
            using (var scope = Container.BeginLifetimeScope())
            {
                var anagramChecker = scope.Resolve<IAnagramChecker>();
                Assert.IsTrue(anagramChecker.IsAnagram("forecast", "FastCORE"));
                Assert.IsTrue(anagramChecker.IsAnagram("MAR", "RAM"));
                Assert.IsFalse(anagramChecker.IsAnagram("forecast", "cast"));
            }
        }
    }
}

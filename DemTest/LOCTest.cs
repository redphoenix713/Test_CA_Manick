using System.Collections.Generic;
using AnagramChecker;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemTest
{
    [TestClass]
    public class LocTest
    {
        private static IContainer Container { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LocCounter>().As<ILocCounter>();
            Container = builder.Build();
        }

        [TestMethod]
        public void Test_LOC_When_Code_Has_Line_Comment()
        {            
            using (var scope = Container.BeginLifetimeScope())
            {
                IEnumerable<string> code = new List<string>
                {
                    "public class Hello {",
                    "public static final void main(String [] args) { // gotta love Java",
                    "// Say hello",
                    "}",
                    "}"
                };
                var locCounter = scope.Resolve<ILocCounter>();
                Assert.AreEqual(4, locCounter.CountLines(code));                
            }
        }

        [TestMethod]
        public void Test_LOC_When_Code_Has_Comment_Block()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                IEnumerable<string> code = new List<string>
                {
                    "public class Hello {",
                    "public static final void main(String [] args) { // gotta love Java",
                    "/* Start Comment",
                    "Test",
                    "Test",
                    "End Comment */",
                    @"System./*wait*/out./*for*/println/*it*/(""Hello/*"");",
                    "}",
                    "}"
                };
                var locCounter = scope.Resolve<ILocCounter>();
                Assert.AreEqual(5, locCounter.CountLines(code));
            }
        }

        [TestMethod]
        public void Test_LOC_Invalid_Empty_Code()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                IEnumerable<string> code = new List<string>
                {                   
                    "/* Start Comment",
                    "Test",
                    "Test",
                    "End Comment */",
                    "",
                    ""                 
                };
                var locCounter = scope.Resolve<ILocCounter>();
                Assert.AreEqual(0, locCounter.CountLines(code));
            }
        }
    }
}

using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace webcrawler
{
    [TestFixture]
    public class Testing
    {
        [Test]
        // Determine GetHref returns string:
        public void TestingStrings()
        {
            Crawler c = new Crawler();
            string seed = "https://hirespace.com";
            //Assert.That(c.StartThreading(seed), Is.TypeOf<string>());
        }

        [Test]
        // Determine whether Collection contains:
        public void TestingCollections()
        {
            MainClass.VisitedList.Add("https://hirespace.com/");
            MainClass.VisitedList.Add("https://hirespace.com/manchester");
            MainClass.VisitedList.Add("https://hirespace.com/london");
            CollectionAssert.DoesNotContain("https://hirespace.com/chelsea", MainClass.VisitedList);
        }

        [Test]
        // Determine exceptions:
        private void TestExceptionIsRaised()
        {
            Crawler c = new Crawler();
            string seed = "testing";
            var ex = Assert.Throws<UnauthorizedAccessException>(() => c.ParseHTML(seed));
            StringAssert.Contains("Attempted to perform an unauthorized operation", ex.Message);
        }

        [Test]
        // Determine if mailto ca
        private void TestStringContains()
        {
            var mailto = "mailto:test@email.com";
            StringAssert.Contains("mailto", mailto);
        }

    }
}
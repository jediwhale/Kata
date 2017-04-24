using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Syterra.Kata.OCR;

namespace Syterra.Kata.OCRTest {
    [TestFixture]
    public class ParserTest {

        [Test]
        public void ParsesOneNumber() {
            AssertParses(new[] {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
                ""
            }, "123456789");
        }

        [Test]
        public void ParsesThreeNumbers() {
            AssertParses(new[] {
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_| _|",
                "",
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  ||_||_|",
                "",
                "    _  _     _  _  _  _  _ ",
                "  | _| _||_||_ |_   ||_||_|",
                "  ||_  _|  | _||_|  | _| _|",
                ""
            }, "123456789,123456788,123456799");
        }

        static void AssertParses(IEnumerable<string> input, string expected) {
            var numbers = Parser.Parse(input);
            var result = string.Join(",", numbers.Select(n => n.Display));
            Assert.AreEqual(expected, result);
        }
    }
}

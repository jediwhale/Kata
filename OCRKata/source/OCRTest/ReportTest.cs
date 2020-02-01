using NUnit.Framework;
using Syterra.Kata.OCR;

namespace Syterra.Kata.OCRTest {
    [TestFixture]
    public class ReportTest {

        [Test]
        public void ReportsValidNumber() {
            Assert.AreEqual("45750800", Report.Write(new TestAccountNumber("45750800", true, true)));
        }

        [Test]
        public void ReportsInvalidNumber() {
            Assert.AreEqual("664371495 ERR", Report.Write(new TestAccountNumber("664371495", false, true)));
        }

        [Test]
        public void ReportsIllegibleNumber() {
            Assert.AreEqual("86110??36 ILL", Report.Write(new TestAccountNumber("86110??36", false, false)));
        }
    }

    class TestAccountNumber: Number {

        public TestAccountNumber(string display, bool isValid, bool isLegible) {
            Display = display;
            IsLegible = isLegible;
            IsValid = isValid;
        }

        public string Display { get; }
        public bool IsLegible { get; }
        public bool IsValid { get; }
    }
}

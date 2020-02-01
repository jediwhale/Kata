using NUnit.Framework;
using Syterra.Kata.OCR;

namespace Syterra.Kata.OCRTest {
    [TestFixture]
    public class ChecksumTest {

        [TestCase("457508000", true)]
        [TestCase("664371495", false)]
        [TestCase("45750?000", false)]
        public void ValidatesChecksum(string input, bool expected) {
            Assert.AreEqual(expected, Checksum.IsValid(input));
        }
        
    }
}
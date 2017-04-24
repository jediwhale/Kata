using NUnit.Framework;
using Syterra.Kata.OCR;

namespace Syterra.Kata.OCRTest {
    [TestFixture]
    public class AccountNumberTest {

        [TestCase(new[] {Glyphs.Four, Glyphs.Two}, "42")]
        [TestCase(new[] {Glyphs.One, Glyphs.Two, Glyphs.Three}, "123")]
        public void IsCompsedOfCharacters(string[] glyphs, string expected) {
            var number = new AccountNumber();
            foreach (var glyph in glyphs) number.Append(glyph);
            Assert.AreEqual(expected, number.ToString());
        }
        
    }
}

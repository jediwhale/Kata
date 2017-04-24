using NUnit.Framework;
using Syterra.Kata.OCR;

namespace Syterra.Kata.OCRTest {
    [TestFixture]
    public class AccountNumberTest {

        [TestCase(new[] {Glyphs.Four, Glyphs.Two}, "42")]
        [TestCase(new[] {Glyphs.One, Glyphs.Two, Glyphs.Three}, "123")]
        public void IsComposedOfCharacters(string[] glyphs, string expected) {
            var number = new AccountNumber();
            foreach (var glyph in glyphs) number.Append(glyph);
            Assert.AreEqual(expected, number.Display);
        }

        [TestCase(new[] {Glyphs.Four, Glyphs.Two}, true)]
        [TestCase(new[] {Glyphs.One, Glyphs.Two, "garbage"}, false)]
        public void HasLegibility(string[] glyphs, bool expected) {
            var number = new AccountNumber();
            foreach (var glyph in glyphs) number.Append(glyph);
            Assert.AreEqual(expected, number.IsLegible);
        }

        [TestCase(new[] {Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Five, Glyphs.One}, true)]
        [TestCase(new[] {Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Five, Glyphs.Two}, false)]
        [TestCase(new[] {Glyphs.One, Glyphs.Two, "garbage"}, false)]
        public void HasValidity(string[] glyphs, bool expected) {
            var number = new AccountNumber();
            foreach (var glyph in glyphs) number.Append(glyph);
            Assert.AreEqual(expected, number.IsValid);
        }


        
    }
}

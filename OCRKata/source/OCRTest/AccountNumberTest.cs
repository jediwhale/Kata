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

        [TestCase(new[] {Glyphs.One, Glyphs.One, Glyphs.One, Glyphs.One, Glyphs.One, Glyphs.One, Glyphs.One, Glyphs.One, Glyphs.One }
            , "711111111")]
        [TestCase(new[] {Glyphs.Seven, Glyphs.Seven, Glyphs.Seven, Glyphs.Seven, Glyphs.Seven, Glyphs.Seven, Glyphs.Seven, Glyphs.Seven, Glyphs.Seven}
            , "777777177")]
        [TestCase(new[] {Glyphs.Two, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero}
            , "200800000")]
        [TestCase(new[] {Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Zero, Glyphs.Five, Glyphs.One}
            , "000000051")]
        [TestCase(new[] {Glyphs.Five, Glyphs.Five, Glyphs.Five, Glyphs.Five, Glyphs.Five, Glyphs.Five, Glyphs.Five, Glyphs.Five, Glyphs.Five}
            , "559555555,555655555")]
        public void HasPossibleDisplays(string[] glyphs, string expected) {
            var number = new AccountNumber();
            foreach (var glyph in glyphs) number.Append(glyph);
            Assert.AreEqual(expected, string.Join(",", number.PossibleDisplays));
        }
    }
}

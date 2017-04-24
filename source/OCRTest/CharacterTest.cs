using NUnit.Framework;
using Syterra.Kata.OCR;

namespace Syterra.Kata.OCRTest {
    [TestFixture]
    public class CharacterTest {

        [TestCase(Glyphs.Zero, "0")]
        [TestCase(Glyphs.One, "1")]
        [TestCase(Glyphs.Two, "2")]
        [TestCase(Glyphs.Three, "3")]
        [TestCase(Glyphs.Four, "4")]
        [TestCase(Glyphs.Five, "5")]
        [TestCase(Glyphs.Six, "6")]
        [TestCase(Glyphs.Seven, "7")]
        [TestCase(Glyphs.Eight, "8")]
        [TestCase(Glyphs.Nine, "9")]
        public void GlyphIsLegible(string glyph, string result) {
            var character = new Character(glyph);
            Assert.IsTrue(character.IsLegible);
            Assert.AreEqual(result, character.ToString());
        }

        [Test]
        public void GlyphIsNotLegible() {
            var character = new Character("garbage");
            Assert.IsFalse(character.IsLegible);
            Assert.AreEqual("?", character.ToString());
        }
    }
}

using System.Collections.Specialized;

namespace Syterra.Kata.OCR {
    public class Character {

        public Character(string ocrGlyph) {
            this.ocrGlyph = ocrGlyph;
        }

        public bool IsLegible => ocrGlyphs.ContainsKey(ocrGlyph);

        public override string ToString() {
            return IsLegible ? ocrGlyphs[ocrGlyph] : "?";
        }

        readonly string ocrGlyph;

        static readonly StringDictionary ocrGlyphs = new StringDictionary {
            {Glyphs.Zero, "0"},
            {Glyphs.One, "1"},
            {Glyphs.Two, "2"},
            {Glyphs.Three, "3"},
            {Glyphs.Four, "4"},
            {Glyphs.Five, "5"},
            {Glyphs.Six, "6"},
            {Glyphs.Seven, "7"},
            {Glyphs.Eight, "8"},
            {Glyphs.Nine, "9"}
        };
    }
}

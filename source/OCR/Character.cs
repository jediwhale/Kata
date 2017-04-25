using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Syterra.Kata.OCR {
    public class Character {

        public Character(string ocrGlyph) {
            this.ocrGlyph = ocrGlyph;
        }

        public bool IsLegible => ocrGlyphs.ContainsKey(ocrGlyph);

        public override string ToString() {
            return IsLegible ? ocrGlyphs[ocrGlyph] : "?";
        }

        public IEnumerable<string> Possibles => ocrGlyphs.Keys.OfType<string>().Where(Matches).Select(g => ocrGlyphs[g]);

        bool Matches(string candidate) {
            return candidate.Where((t, i) => t != ocrGlyph[i]).Count() < 2;
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

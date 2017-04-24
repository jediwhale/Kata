using System.Collections.Generic;
using System.Linq;

namespace Syterra.Kata.OCR {
    public class AccountNumber {
        public void Append(string glyph) {
            characters.Add(new Character(glyph));
        }

        public override string ToString() {
            return string.Join(string.Empty, characters.Select(c => c.ToString()));
        }

        readonly List<Character> characters = new List<Character>();
    }
}

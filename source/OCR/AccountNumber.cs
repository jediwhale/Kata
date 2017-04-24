using System.Collections.Generic;
using System.Linq;

namespace Syterra.Kata.OCR {
    public class AccountNumber: Number {
        public void Append(string glyph) {
            characters.Add(new Character(glyph));
        }

        public string Display => string.Join(string.Empty, characters.Select(c => c.ToString()));

        public bool IsLegible => characters.All(c => c.IsLegible);

        public bool IsValid => IsLegible && Checksum.IsValid(Display);

        readonly List<Character> characters = new List<Character>();
    }
}

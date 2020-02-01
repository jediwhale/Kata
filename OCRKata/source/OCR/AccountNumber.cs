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

        public IEnumerable<string> PossibleDisplays {
            get {
                if (IsValid) {
                    yield return Display;
                }
                else {
                    for (var i = 0; i < characters.Count; i++) {
                        var newDisplay = new List<string>(characters.Select(c => c.ToString()));
                        foreach (var possible in characters[i].Possibles) {
                            newDisplay[i] = possible;
                            var newNumberDisplay = string.Join(string.Empty, newDisplay);
                            if (Checksum.IsValid(newNumberDisplay)) yield return newNumberDisplay;
                        }
                    }
                }
            }
        }

        readonly List<Character> characters = new List<Character>();
    }
}

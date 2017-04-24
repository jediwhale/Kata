using System.Collections.Generic;

namespace Syterra.Kata.OCR {
    public class Parser {
        public static IEnumerable<AccountNumber> Parse(IEnumerable<string> input) {
            var lines = new List<string>();
            foreach (var line in input) {
                if (lines.Count < 3) {
                    lines.Add(line);
                }
                else if (lines.Count == 3) {
                    var number = new AccountNumber();
                    for (var digit = 0; digit < 9; digit++) {
                        number.Append(
                            lines[0].Substring(digit * 3, 3) +
                            lines[1].Substring(digit * 3, 3) +
                            lines[2].Substring(digit * 3, 3));
                    }
                    yield return number;
                    lines.Clear();
                }
            }
        }
    }
}

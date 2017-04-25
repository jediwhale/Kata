namespace Syterra.Kata.OCR {
    public class Checksum {
        public static bool IsValid(string input) {
            var sum = 0;
            var count = 9;
            foreach (var digit in input) {
                if (digit == '?') return false;
                sum += (digit - '0') * count;
                if (count == 1) break;
                count--;
            }
            return sum % 11 == 0;
        }
    }
}

namespace Syterra.Kata.OCR {
    public class Report {
        public static string Write(Number number) {
            return number.Display +
                (number.IsValid
                    ? string.Empty
                    : " " + (number.IsLegible ? "ERR" : "ILL"));
        }
    }
}

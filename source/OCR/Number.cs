namespace Syterra.Kata.OCR {
    public interface Number {
        string Display { get; }
        bool IsLegible { get; }
        bool IsValid { get; }
    }
}

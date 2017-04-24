using System;
using System.IO;
using System.Linq;
using Syterra.Kata.OCR;

namespace Syterra.Kata.OCRProgram {
    class Program {
        static void Main(string[] args) {
            foreach (var output in Parser.Parse(File.ReadAllLines(args[0])).Select(Report.Write)) {
                Console.WriteLine(output);
            }
        }
    }
}

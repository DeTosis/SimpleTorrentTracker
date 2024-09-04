using static System.Console;

namespace AngleSharpTests.View {
    public static class ConsoleDisplayFeatures {
        public static void DrawSplitter(int lenght = 20) {
            WriteLine(string.Concat(Enumerable.Repeat("-", lenght)));
        }
    }
}

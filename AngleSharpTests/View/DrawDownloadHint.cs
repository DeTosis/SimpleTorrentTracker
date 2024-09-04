using static System.Console;
using static AngleSharpTests.View.ConsoleDisplayFeatures;

namespace AngleSharpTests.View {
    public class DrawDownloadHint {
        public void DrawHint() {
            DrawSplitter();

            WriteLine("+ Type:");
            WriteLine("+ [-d] Download File");
            WriteLine("+ [-b] Return to menu");

            DrawSplitter();
        }
    }
}

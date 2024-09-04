using static System.Console;
using static AngleSharpTests.View.ConsoleDisplayFeatures;

namespace AngleSharpTests.View
{
    public  class DisplayReturningOptions {
        public void Display() {
            DrawSplitter();

            WriteLine("+ Would you like to start new search? Type:");
            WriteLine("+ [y] To start new search");
            WriteLine("+ [n] To return to existing games list");

            DrawSplitter();
        }
    }
}

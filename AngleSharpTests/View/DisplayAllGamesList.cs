using static System.Console;
using static AngleSharpTests.View.ConsoleDisplayFeatures;

namespace AngleSharpTests.View {
    public class DisplayAllGamesList {
        public void Display(Dictionary<string,string> gamesList) {
            int counter = 0;
            foreach (var game in gamesList) {
                counter++;
                if (counter < 10) {
                    WriteLine($" [{counter}] {game.Key}");
                } else {
                    WriteLine($"[{counter}] {game.Key}");
                }
            }
            DrawSplitter();
        }
    }
}

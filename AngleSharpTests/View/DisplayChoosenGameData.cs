using static System.Console;
using static AngleSharpTests.View.ConsoleDisplayFeatures;

namespace AngleSharpTests.View {
    public class DisplayChoosenGameData {
        public void Display(GameDataObject data) {
            DrawSplitter();

            WriteLine($"\n>> Game Name: {data.gameName}");
            WriteLine($"\n===== Game Info =====");
            WriteLine($">> Game Size: {data.gameSize}");
            WriteLine($">> Last updated: {data.lastUpdateTime}");
            WriteLine($"\n===== Torrent Info =====");
            WriteLine($">> Icon Linq: {data.gameIcoURI}");
            WriteLine($">> Torrent Linq: {data.torrentURI}");

            DrawDownloadHint drawDownloadHint = new();
            drawDownloadHint.DrawHint();
        }
    }
}

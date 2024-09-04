using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace AngleSharpTests.Igruha {
    public class Igruha_ParseChoosenGame {
        string gameName = "";
        string torrentURI = "";
        bool isCracked = false;

        string gameIcoURI = "";
        string tabletType = "";
        string gameSize = "";
        string lastUpdateTime = "";

        public async Task<GameDataObject> FetchAllData(int choosenGame, Dictionary<string, string> gamesLinqs) {
            if (choosenGame < 0) return null;

            string gameLinq;
            int counter = 0;
            foreach (var game in gamesLinqs) {
                counter++;
                if (counter == choosenGame) {
                    gameLinq = game.Value;
                    await ParseGame(gameLinq);
                }
            }

            return new GameDataObject(
                gameName, torrentURI, isCracked, gameIcoURI, tabletType, gameSize, lastUpdateTime);
        }

        async Task ParseGame(string gameLinq) {
            string sellector = "";
            IDocument page = await GetGamePage(gameLinq);

            sellector = "#dle-content > div.module-title > h1";
            gameName = page.QuerySelector(sellector)?.TextContent ?? "";

            sellector = "#article-film-full-poster-bg > img";
            gameIcoURI = page.QuerySelector(sellector)?.GetAttribute("src") ?? "";

            sellector = "#dle-content > div:nth-child(3) > b:nth-child(11)";
            tabletType = page.QuerySelector(sellector)?.TextContent ?? "";

            sellector = "#dle-content > div.blockinfo > center > span > span";
            gameSize = page.QuerySelector(sellector)?.TextContent ?? "";

            sellector = "#article-film-full-info > span > time";
            lastUpdateTime = page.QuerySelector(sellector)?.TextContent ?? "";

            //Get linq & bool
            await GetTorrentLinq(page, gameLinq);
        }

        async Task GetTorrentLinq(IDocument page, string gamePage) {
            string sellector = "#navbartor > center > a";
            try {
                string linq = page.QuerySelector<IHtmlAnchorElement>(sellector)?.Href ?? "";
                page = await GetGamePage(linq);
                sellector = "#navbartorrent > noindex > center > a";
                linq = page.QuerySelector<IHtmlAnchorElement>(sellector)?.Href ?? "";
                if (!string.IsNullOrEmpty(linq)) {
                    isCracked = true;
                    torrentURI = linq;
                }
            } catch (Exception e) {
                isCracked = false;
                torrentURI = "";
            }
        }

        async Task<IDocument> GetGamePage(string gameLinq) {
            IBrowsingContext context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            IDocument queryDocument = await context.OpenAsync(gameLinq);
            return queryDocument;
        }
    }
}

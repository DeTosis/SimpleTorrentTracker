using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace AngleSharpTests.Igruha
{
    public class Igruha_GameListPageParser
    {

        readonly string Q_gamesBlock = ".article-film1";
        readonly string Q_hrefBlock = ".article-film1 > div.article-film-image > a";
        readonly string Q_nameBlock = ".article-film1 > div.article-film-image > a > img.article-img";

        public Dictionary<string, string> gameLinqs { get; } = new();

        public void ParsePage(IDocument page)
        {
            var GamesOnPage = page.QuerySelectorAll(Q_gamesBlock);
            try
            {
                foreach (var game in GamesOnPage)
                {
                    var gameHref = game.QuerySelector(Q_hrefBlock) as IHtmlAnchorElement;
                    var gameName = game.QuerySelector(Q_nameBlock);

                    gameLinqs.Add(gameName.GetAttribute("title") ?? "", gameHref.Href ?? "");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

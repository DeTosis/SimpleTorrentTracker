using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace AngleSharpTests.Igruha {
    public class Igruha_PagesProcessor {
        readonly string _searchPageURL = "https://itorrents-igruha.org/index.php?do=search";
        readonly string Q_fullSearch = "div.articles > div#dle-content > div#searchtable > form";
        readonly string Q_divPages = "#page-switchers > #pages>a";

        List<IDocument> gamePages = new();

        public async Task<List<IDocument>> ParseAllGames(string searchRequest) {
            await GetPagesCount(searchRequest);
            return gamePages;
        }

        private async Task GetPagesCount(string searchRequest) {
            IBrowsingContext context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            IDocument queryDocument = await context.OpenAsync(_searchPageURL);
            IHtmlFormElement? searchForm = queryDocument.QuerySelector<IHtmlFormElement>(Q_fullSearch);

            IDocument mainPageDoc = await GetPageDocument(searchForm ?? throw new Exception("Cannot find search form"), searchRequest);
            var switchPages = mainPageDoc.QuerySelectorAll(Q_divPages).OfType<IHtmlAnchorElement>();

            if (switchPages.Count() < 1) {
                gamePages.Add(mainPageDoc);
                return;
            }

            for (int i = 1; i < switchPages.Count() + 2; i++) {
                gamePages.Add(await GetPageDocument(searchForm ?? throw new Exception("Cannot find search form"), searchRequest, i));
            }
        }

        private async Task<IDocument> GetPageDocument(IHtmlFormElement searchForm, string searchRequest, int searchPage = 1) {
            IDocument resultDocument = await searchForm.SubmitAsync(fields: new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
                ["do"] = "search",
                ["subaction"] = "search",
                ["story"] = $"{searchRequest}",
                ["search_start"] = $"{searchPage}",
                ["result_from"] = $"{searchPage * 20}"
            });
            return resultDocument;
        }
    }
}

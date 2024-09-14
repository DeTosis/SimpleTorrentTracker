using System.Text;
using AngleSharp.Dom;
using AngleSharpTests;
using AngleSharpTests.Igruha;
using AngleSharpTests.View;

class Program {
    static Encoding win1251 = CodePagesEncodingProvider.Instance.GetEncoding(1251) ?? Encoding.UTF8;
    static string userInput = "";
    static string GameTitile;

    static Igruha_PagesProcessor processor = new();
    static Igruha_GameListPageParser pageProcessor = new();
    public static async Task Main() {
        //Time stamp
        Console.OutputEncoding = win1251;
        Console.InputEncoding = win1251;
        DateTime timeStart = DateTime.Now;
        //=== ===

        await GameNameUserInput();

        //Time stamp
        DateTime timeEnd = DateTime.Now;
        var h = timeEnd - timeStart;
        Console.WriteLine($"fetching Time: {h}");
        //=== ===
    }

    static async Task GameNameUserInput() {
        //Get UserInput for game name
        Console.Write(":: > Game Name: ");
        userInput = Console.ReadLine() ?? "";
        GameTitile = userInput;
        await ProcessPages();
    }

    static async Task ProcessPages() {
        //Get Pages
        List<IDocument> DocList = await processor.ParseAllGames(userInput);
        //Parse All Pages
        foreach (IDocument doc in DocList) {
            pageProcessor.ParsePage(doc);
        }
        if (pageProcessor.gameLinqs.Count() < 1) {
            Console.WriteLine(":: > No Games Found With This Title");
            await ReturnAtTheStartOrMiddle();
            return;
        }

        await DisplayFetchedGames();
    }

    static async Task DisplayFetchedGames() {
        //Display Fetched Games
        DisplayAllGamesList displayAllGamesList = new DisplayAllGamesList();
        displayAllGamesList.Display(pageProcessor.gameLinqs);
        Console.Write("Enter game number to inspect it: ");
        await GetGameToShowInput();
    }

    static async Task GetGameToShowInput() {
        //Get Input
        userInput = Console.ReadLine() ?? "";
        int choosenGame = -1;
        try {
            choosenGame = Convert.ToInt32(userInput);
            if (choosenGame < 1 || choosenGame > pageProcessor.gameLinqs.Count()) throw new Exception("[Invalid input]");

        } catch (Exception e) {
            Console.WriteLine(e.Message);
            await ReturnAtTheStartOrMiddle();
            return;
        }

        await ProcessChoosenGame(choosenGame);
    }
    static async Task ProcessChoosenGame(int choosenGame) {
        //Process choosenGame
        Igruha_ParseChoosenGame parseGame = new();
        GameDataObject data = await parseGame.FetchAllData(choosenGame, pageProcessor.gameLinqs);

        //Display choosen game data
        DisplayChoosenGameData displayData = new DisplayChoosenGameData();
        displayData.Display(data);

        await GetInputOnDownload(data);
    }

    static async Task GetInputOnDownload(GameDataObject data) {
        //Get Input On Download
        Console.Write("> Input:");
        userInput = Console.ReadLine() ?? "";
        switch (userInput) {
            case "-d":
                if (!data.isCracked || string.IsNullOrEmpty(data.torrentURI)) {
                    Console.WriteLine("Torrent not found or does not exist");
                    await ReturnAtTheStartOrMiddle();
                    return;
                }

                string downloadsPath = @"C:\Users\USER_NAME\Downloads".Replace("USER_NAME", Environment.UserName);

                DownloadTorrent loader = new();
                Console.WriteLine("Downloading torrent file...");
                await loader.Download(data.torrentURI, data.gameName, downloadsPath);
                Console.WriteLine("Done !");

                System.Diagnostics.Process.Start("explorer.exe", $"{downloadsPath}");

                await ReturnAtTheStartOrMiddle();
                break;
            case "-b":
                await ReturnAtTheStartOrMiddle();
                break;
            default:
                Console.WriteLine("Invalid Input");
                await GetInputOnDownload(data);
                break;
        }
    }

    static async Task ReturnAtTheStartOrMiddle() {
        DisplayReturningOptions displayReturningOptions = new DisplayReturningOptions();
        displayReturningOptions.Display();

        userInput = Console.ReadLine() ?? "";
        switch (userInput) {
            case "y":
                Console.Clear();
                pageProcessor = new();
                processor = new();
                await GameNameUserInput();
                break;
            case "n":
                Console.Clear();
                Console.WriteLine("\n> Input Search: " + GameTitile + " \n");
                await DisplayFetchedGames();
                break;
            default :
                Console.WriteLine(":: > Invalid Input");
                await ReturnAtTheStartOrMiddle();
                break;
        }
    }
}
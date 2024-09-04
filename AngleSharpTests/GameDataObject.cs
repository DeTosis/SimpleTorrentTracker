namespace AngleSharpTests {
    public record GameDataObject {

        //Required fields
        public string gameName { get; private set; }
        public string torrentURI { get; private set; }
        public bool isCracked { get; private set; }

        //Visual
        public string gameIcoURI { get; private set; }

        //Description fields
        public string tabletType { get; private set; }
        public string gameSize { get; private set; }
        public string lastUpdateTime { get; private set; }

        public GameDataObject(
            string gameName, string torrentURI, bool isCracked,
            string gameIcoURI = "",
            string tabletType = "", string gameSize="", string lastUpdateTime="") {
            
            this.gameName = gameName;
            this.torrentURI = torrentURI;
            this.isCracked = isCracked;
            this.gameIcoURI = gameIcoURI;
            this.tabletType = tabletType;
            this.gameSize = gameSize;
            this.lastUpdateTime = lastUpdateTime;
        }
    }
}

namespace AngleSharpTests {
    public class DownloadTorrent {
        public async Task Download(string url, string gameName, string downloadPath) {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode) {
                Console.WriteLine("Error: " + response.StatusCode);
                return;
            }

            var totalBytes = response.Content.Headers.ContentLength ?? -1L;
            var canReportProgress = totalBytes != -1;
            var totalBytesRead = 0L;
            var readChunkSize = 8192;

            using (var contentStream = await response.Content.ReadAsStreamAsync())
            using (var fileStream = new FileStream($"{downloadPath}/{gameName}.torrent",
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                readChunkSize,
                true)) {

                var buffer = new byte[readChunkSize];
                int bytesRead;

                while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0) {
                    await fileStream.WriteAsync(buffer, 0, bytesRead);
                    totalBytesRead += bytesRead;
                }
            }
        }
    }
}

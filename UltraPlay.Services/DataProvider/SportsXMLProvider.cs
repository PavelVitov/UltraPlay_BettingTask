using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using UltraPlay.Services.Interfaces;
using UltraPlay.Services.Options;

namespace UltraPlay.Services.DataProvider
{
    public class SportsXMLProvider : ISportsXMLProvider
    {
        private readonly HttpClient _client;
        private readonly ILogger<SportsXMLProvider> _logger;
        private readonly UrlOptions _urlOptions;

        public SportsXMLProvider(HttpClient client, ILogger<SportsXMLProvider> logger, IOptions<UrlOptions> urlOptions)
        {
            _client = client;
            _logger = logger;
            _urlOptions = urlOptions.Value;
        }

        public async Task<string> GetData(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            var content = await _client.GetStringAsync(_urlOptions.Endpoint);
            if(string.IsNullOrWhiteSpace(content))
            {
                throw new InvalidDataException("Could not fetch sports xml data.");
            }
            _logger.LogInformation("Fetched sports data from url.");
            return content;
        }
    }
}

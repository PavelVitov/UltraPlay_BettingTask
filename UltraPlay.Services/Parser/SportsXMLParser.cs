using Microsoft.Extensions.Logging;

using System.Xml.Serialization;

using UltraPlay.Data.DTOs;
using UltraPlay.Services.Interfaces;

namespace UltraPlay.Services.Parser;

public class SportsXMLParser : ISportsXMLParser<SportDTO[]>
{
    private readonly ILogger<SportsXMLParser> _logger;

    public SportsXMLParser(ILogger<SportsXMLParser> logger)
    {
        _logger = logger;
    }


    public SportDTO[] Parse(string content)
    {

        if (string.IsNullOrWhiteSpace(content))
        {
            throw new InvalidDataException("The provided sports xml is null, empty or whitespace.");
        }

        var reader = new StringReader(content);
        var root = new XmlRootAttribute
        {
            ElementName = "XmlSports",
            IsNullable = true
        };

        var serializer = new XmlSerializer(typeof(SportDTO[]), root);
        var parsedData = (SportDTO[]?)serializer.Deserialize(reader);
        if (parsedData == null)
        {
            throw new InvalidDataException("Could not deserialize sports xml.");
        }
        _logger.LogInformation("Sports xml parsed successfully.");
        return parsedData;
    }
}

using System.Xml.Serialization;

namespace UltraPlay.Data.DTOs;

[XmlType("Match")]
public class MatchDTO : AbstractDTO
{
    [XmlAttribute("StartDate")]
    public DateTime StartDate { get; set; }

    [XmlAttribute("MatchType")]
    public string MatchType { get; set; }

    [XmlElement("Bet")]
    public BetDTO[] Bets { get; set; }
}
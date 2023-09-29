using System.Xml.Serialization;

namespace UltraPlay.Data.DTOs;

[XmlType("Bet")]
public class BetDTO : AbstractDTO
{
    [XmlAttribute("IsLive")]
    public bool IsLive { get; set; }

    [XmlElement("Odd")]
    public OddDTO[] Odds { get; set; }
}
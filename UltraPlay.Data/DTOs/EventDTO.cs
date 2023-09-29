using System.Xml.Serialization;

namespace UltraPlay.Data.DTOs;

[XmlType("Event")]
public sealed class EventDTO : AbstractDTO
{
    [XmlAttribute("IsLive")]
    public bool IsLive { get; set; }

    [XmlAttribute("CategoryID")]
    public int CategoryID { get; set; }

    [XmlElement("Match")]
    public MatchDTO[] Matches { get; set; }
}
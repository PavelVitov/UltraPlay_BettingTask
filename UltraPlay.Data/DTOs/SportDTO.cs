using System.Xml.Serialization;

namespace UltraPlay.Data.DTOs;

[XmlType("Sport")]
public sealed class SportDTO : AbstractDTO
{
    [XmlElement("Event")]
    public EventDTO[] Events { get; set; }
}

using System.Xml.Serialization;

namespace UltraPlay.Data.DTOs;

[XmlType("Odd")]
public class OddDTO : AbstractDTO
{
    [XmlAttribute("Value")]
    public double Value { get; set; }

    [XmlAttribute("SpecialBetValue")]
    public string SpecialBetValue { get; set; }
}
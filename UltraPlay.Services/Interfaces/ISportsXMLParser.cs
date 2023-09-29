namespace UltraPlay.Services.Interfaces;

public interface ISportsXMLParser<T>
{
    T Parse(string content);
}

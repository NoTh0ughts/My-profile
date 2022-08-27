using HtmlAgilityPack;

namespace MyProfile.Services.Timed_Worker.Extensions;

public static class HtmlParsingExtensions
{
    public static string GetElementSingleValue(this HtmlDocument html, string elName)
    {
        var valueElement = html.GetElementbyId(elName).InnerText;
        if (valueElement is null)
        {
            throw new NullReferenceException($"Не найден елемент с id {elName}");
        }

        return valueElement.Trim().TrimStart('#');                                   
    }
    
    public static IEnumerable<string> GetElementListValue(this HtmlDocument html, string elName, char separator = '*')
    {
        return html.GetElementSingleValue(elName)
            .Split(separator)                   // Split string 
            .Select(x => x.Trim())         // Trim every string for remove extra char`s
            .Skip(1);                           // Coz using Split function, first element skipped
    }
}
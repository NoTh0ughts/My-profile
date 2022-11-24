using HtmlAgilityPack;
using MyProfile.Services.Timed_Worker.Extensions;

namespace Tests;

public class HtmlParsing_Tests
{
    [Theory]
    [InlineData("<div id=\"one\"> * Hello World</div>")]
    [InlineData("<div id=\"one\"> * Hello World</div><div id=\"123\"> * Hello World</div>")]
    public void GetElementListValue_Test(string html)
    {
        var expected = "Hello World";
        
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);
        
        Assert.False(string.IsNullOrEmpty(doc.Text));
        Assert.Equal(doc.GetElementListValue("one").FirstOrDefault(), expected);
    }
    
    [Theory]
    [InlineData("<div id=\"one\">Hello World</div>")]
    [InlineData("<div id=\"one\">Hello World</div><div id=\"one\">Hello World</div>")]
    [InlineData("<div id=\"one\">Hello World</div><div id=\"two\">Hello World</div>")]
    [InlineData("<p id=\"one\">Hello World</div><div id=\"two\">Hello World</div>")]
    [InlineData("<a id=\"one\">Hello World</div>")]
    public void GetElementSingleValue_Test(string html)
    {
        var expected = "Hello World";
        
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);
        
        Assert.False(string.IsNullOrEmpty(doc.Text));
        Assert.Equal(expected, doc.GetElementSingleValue("one"));
    }
}
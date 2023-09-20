using System.Net;

using Moq;
using Moq.Protected;

namespace Boldem.ConsoleApp.Tests.Clients.Helpers;

public class HttpClientSpyBuilder
{
    private Uri? _uri;

    private HttpContent? _content;

    private Action<HttpRequestMessage, CancellationToken>? _callback;

    public HttpClientSpyBuilder SetBaseUri(Uri uri)
    {
        _uri = uri;
        
        return this;
    }
    
    public HttpClientSpyBuilder SetContent(HttpContent content)
    {
        _content = content;
        
        return this;
    }
    
    public HttpClientSpyBuilder SetCallback(Action<HttpRequestMessage, CancellationToken> callback)
    {
        _callback = callback;
        
        return this;
    }

    public HttpClient Build()
    {
        ArgumentNullException.ThrowIfNull(_uri);
        ArgumentNullException.ThrowIfNull(_callback);
        
        var mockMessageHandler = new Mock<HttpMessageHandler>();
        mockMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Callback(_callback)
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = _content
            });

        return new HttpClient(mockMessageHandler.Object)
        {
            BaseAddress = _uri
        };
    }
}
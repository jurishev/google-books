namespace GoogleBooks.Tests;

internal class HttpMessageHandlerMock : HttpMessageHandler
{
    private readonly string url;
    private readonly HttpResponseMessage response;

    public HttpMessageHandlerMock()
    {
        this.url = string.Empty;
        this.response = new HttpResponseMessage();
    }

    public HttpMessageHandlerMock(string url, HttpResponseMessage response)
    {
        this.url = url;
        this.response = response;
    }

    public bool RequestPerformed { get; private set; }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        this.RequestPerformed = true;

        return Task.FromResult(this.url == request.RequestUri?.OriginalString ?
            this.response : new HttpResponseMessage(System.Net.HttpStatusCode.NotFound));
    }
}

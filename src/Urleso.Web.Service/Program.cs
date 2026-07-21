using System.Net;
using Microsoft.Extensions.Options;
using Urleso.Web.Service.Api;
using Yarp.ReverseProxy.Forwarder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiSettings();
builder.Services.AddHttpForwarder();

var app = builder.Build();

var apiBaseAddress = app.Services.GetRequiredService<IOptions<ApiSettings>>().Value.BaseAddress;

var forwarderInvoker = new HttpMessageInvoker(new SocketsHttpHandler
{
    UseProxy = false,
    AllowAutoRedirect = false,
    AutomaticDecompression = DecompressionMethods.None,
    // this handler is shared by every request, so its cookie container would be shared too
    UseCookies = false,
    ConnectTimeout = TimeSpan.FromSeconds(15),
    // without a lifetime the pooled connection pins the API's IP across container restarts
    PooledConnectionLifetime = TimeSpan.FromMinutes(2)
});

// The whole request path is appended to the destination, so "/api/shortened-urls" maps 1:1 upstream
app.MapForwarder("/api/{**catch-all}", apiBaseAddress, ForwarderRequestConfig.Empty, HttpTransformer.Default,
    forwarderInvoker);

// Do not add UseBlazorFrameworkFiles(): its content encoding negotiator runs ahead of routing and 500s
app.MapStaticAssets();
app.MapFallbackToFile("index.html");

app.Run();

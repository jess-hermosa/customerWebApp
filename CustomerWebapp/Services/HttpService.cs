
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

internal interface IHttpService : IDisposable
{
    Task<T> GetAsync<T>(string url);
    Task<TReturn> PostAsync<T, TReturn>(string url, T content);
    Task<TReturn> DeleteAsync<T, TReturn>(string url);
}

internal class HttpService : IHttpService
{
    private readonly HttpClient _httpClient;

    public HttpService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<T> GetAsync<T>(string url)
    {
        HttpResponseMessage response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(responseBody);
    }

    public async Task<TReturn> PostAsync<T, TReturn>(string url, T content)
    {
        HttpContent httpContent = null;

        if (content != null)
        {
            var ms = new MemoryStream();
            StreamJsonSerializer(content, ms);
            ms.Seek(0, SeekOrigin.Begin);
            httpContent = new StreamContent(ms);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        HttpResponseMessage response = await _httpClient.PostAsync(url, httpContent);
        response.EnsureSuccessStatusCode();

        string responseBody = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TReturn>(responseBody);
    }

    public async Task<TReturn> DeleteAsync<T, TReturn>(string url)
    {
        HttpResponseMessage response = await _httpClient.DeleteAsync(url);

        string responseBody = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TReturn>(responseBody);
    }

    private static void StreamJsonSerializer(object value, Stream stream)
    {
        using (var streamWriter = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
        using (var jsonTextWriter = new JsonTextWriter(streamWriter) { Formatting = Formatting.None })
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(jsonTextWriter, value);
            jsonTextWriter.Flush();
        }
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}
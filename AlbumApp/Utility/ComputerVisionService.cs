using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AlbumApp.Utility
{
    public class ComputerVisionService : IComputerVisionService
    {   
        
        private readonly IOptions<APIConfig> _config;
        private readonly ILogger<ComputerVisionService> _logger;

        public ComputerVisionService(IOptions<APIConfig> config, ILogger<ComputerVisionService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<string> callAPI(byte[] image, string requestParameters)
        {   
            try
            {
                string url = $"{_config.Value.Endpoint}vision/v3.0/analyze?visualFeatures={requestParameters}";

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _config.Value.Key);

                HttpResponseMessage response;
                using (ByteArrayContent content = new ByteArrayContent(image))
                {
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    response = await client.PostAsync(url, content);
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return null;
            }
        }
    }
}
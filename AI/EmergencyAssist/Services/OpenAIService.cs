using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System;

namespace EmergencyAssist.Services
{
    public class OpenAIService
    {
        private readonly string _apiKey;
        private readonly string _endpoint;
        private readonly string _deployment;
        private readonly HttpClient _client;

        public OpenAIService(IConfiguration config)
        {
            _apiKey = config["Azure:OpenAIApiKey"] ?? throw new ArgumentNullException("OpenAI API Key is not configured.");
            _endpoint = config["Azure:OpenAIEndpoint"] ?? throw new ArgumentNullException("OpenAI Endpoint is not configured.");
            _deployment = config["Azure:DeploymentName"] ?? throw new ArgumentNullException("Deployment Name is not configured.");

            _client = new HttpClient();
            _client.BaseAddress = new Uri(_endpoint);
            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.Add("api-key", _apiKey);
        }

        public async Task<string?> GetChatResponseAsync(string prompt)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt cannot be null or empty", nameof(prompt));

            var requestBody = new
            {
                messages = new[] {
                    new { role = "user", content = prompt }
                },
                temperature = 0.7,
                max_tokens = 1000
            };

            var requestUri = $"openai/deployments/{_deployment}/chat/completions?api-version=2023-05-15";
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync(requestUri, content);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();

                using var doc = JsonDocument.Parse(result);
                if (doc.RootElement.TryGetProperty("choices", out var choices) && choices.GetArrayLength() > 0)
                {
                    var messageContent = choices[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString();

                    return messageContent ?? "No content returned by OpenAI.";
                }
                else
                {
                    return "No valid response from OpenAI.";
                }
            }
            catch (HttpRequestException e)
            {
                throw new InvalidOperationException("Error calling the OpenAI API.", e);
            }
            catch (JsonException e)
            {
                throw new InvalidOperationException("Error parsing the response from OpenAI.", e);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("An unexpected error occurred.", e);
            }
        }
    }
}


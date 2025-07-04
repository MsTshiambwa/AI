using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace EmergencyAssist.Services
{
    public class AzureMapsService
    {
        private readonly string _key;
        private readonly HttpClient _client;

        public AzureMapsService(IConfiguration config, HttpClient client)
        {
            _key = config["Azure:MapsKey"] ?? throw new ArgumentNullException("Azure Maps API Key is not configured.");
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string?> ReverseGeocodeAsync(double latitude, double longitude)
        {
            var url = $"https://atlas.microsoft.com/search/address/reverse/json?api-version=1.0&query={latitude},{longitude}&subscription-key={_key}";

            try
            {
                var response = await _client.GetStringAsync(url);
                var jsonDocument = JsonDocument.Parse(response);

                if (jsonDocument.RootElement.TryGetProperty("addresses", out var addresses) &&
                    addresses.ValueKind == JsonValueKind.Array &&
                    addresses.GetArrayLength() > 0)
                {
                    var addressElement = addresses[0];

                    if (addressElement.TryGetProperty("address", out var addressObj) &&
                        addressObj.TryGetProperty("freeformAddress", out var freeform))
                    {
                        return freeform.GetString();
                    }
                }

                return null;
            }
            catch (HttpRequestException e)
            {
                throw new InvalidOperationException("Error calling the Azure Maps API.", e);
            }
            catch (JsonException e)
            {
                throw new InvalidOperationException("Error parsing the response from Azure Maps.", e);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("An unexpected error occurred.", e);
            }
        }
    }
}

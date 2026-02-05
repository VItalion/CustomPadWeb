using CustomPadWeb.Common.ViewModels;
using System.Net.Http.Json;

namespace CustomPadWeb.Frontend.Client.Clients
{
    public class ConfigurationApiClient
    {
        private readonly HttpClient _http;

        public ConfigurationApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<CustomPadViewModel>> GetAllAsync(int range = 0, int skip = 0)
        {
            var url = $"/api/configuration/?range={range}&skip={skip}";
            return await _http.GetFromJsonAsync<IEnumerable<CustomPadViewModel>>(url).ConfigureAwait(false) ?? Enumerable.Empty<CustomPadViewModel>();
        }

        public async Task<CustomPadViewModel?> GetByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<CustomPadViewModel>($"/api/configuration/{id}").ConfigureAwait(false);
        }

        public async Task CreateAsync(CustomPadViewModel vm)
        {
            var response = await _http.PostAsJsonAsync("/api/configuration/", vm).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(Guid id, CustomPadViewModel vm)
        {
            var response = await _http.PutAsJsonAsync($"/api/configuration/?id={id}", vm).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"/api/configuration/{id}").ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
    }
}

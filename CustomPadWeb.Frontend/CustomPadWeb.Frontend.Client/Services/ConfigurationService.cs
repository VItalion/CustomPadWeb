using CustomPadWeb.Frontend.Client.Clients;
using CustomPadWeb.Common.ViewModels;

namespace CustomPadWeb.Frontend.Client.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly ConfigurationApiClient _client;

        public ConfigurationService(ConfigurationApiClient client)
        {
            _client = client;
        }

        public Task CreateAsync(CustomPadViewModel vm) => _client.CreateAsync(vm);

        public Task DeleteAsync(Guid id) => _client.DeleteAsync(id);

        public Task<IEnumerable<CustomPadViewModel>> GetAllAsync() => _client.GetAllAsync();

        public Task<CustomPadViewModel?> GetByIdAsync(Guid id) => _client.GetByIdAsync(id);

        public Task UpdateAsync(Guid id, CustomPadViewModel vm) => _client.UpdateAsync(id, vm);
    }
}

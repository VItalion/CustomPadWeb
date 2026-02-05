using CustomPadWeb.Common.ViewModels;

namespace CustomPadWeb.Frontend.Client.Services
{
    public interface IConfigurationService
    {
        Task<IEnumerable<CustomPadViewModel>> GetAllAsync();
        Task<CustomPadViewModel?> GetByIdAsync(Guid id);
        Task CreateAsync(CustomPadViewModel vm);
        Task UpdateAsync(Guid id, CustomPadViewModel vm);
        Task DeleteAsync(Guid id);
    }
}

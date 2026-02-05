using CustomPadWeb.Common.ViewModels;

namespace CustomPadWeb.Application.Services.Contracts
{
    public interface ICustomConfigurationService
    {
        Task<IEnumerable<CustomPadViewModel>> GetAllAsync(int range = 0, int skip = 0);
        Task<CustomPadViewModel?> GetByIdAsync(Guid id);
        Task CreateAsync(CustomPadViewModelBase vm);
        Task UpdateAsync(Guid id, UpdatePadViewModel vm);
        Task DeleteAsync(Guid id);
    }
}

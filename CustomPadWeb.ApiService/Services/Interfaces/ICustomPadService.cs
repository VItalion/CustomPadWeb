using CustomPadWeb.Common.ViewModels;

namespace CustomPadWeb.ApiService.Services.Interfaces
{
    public interface ICustomPadService
    {
        Task<Guid> CreatePadAsync(CustomPadViewModelBase viewModel);

        Task DeletePadAsync(Guid id);

        Task<CustomPadViewModel> GetByIdAsync(Guid id);

        Task<IEnumerable<CustomPadViewModel>> GetAsync();

        Task<CustomPadViewModel> UpdateAsycn(Guid id, UpdatePadViewModel viewModel);
    }
}

using CustomPadWeb.ApiService.Services.Interfaces;
using CustomPadWeb.Common.ViewModels;

namespace CustomPadWeb.ApiService.Services
{
    public class CustomPadService : ICustomPadService
    {
        public Task<Guid> CreatePadAsync(CustomPadViewModelBase viewModel)
        {
            throw new NotImplementedException();
        }

        public Task DeletePadAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomPadViewModel>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomPadViewModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CustomPadViewModel> UpdateAsycn(Guid id, UpdatePadViewModel viewModel)
        {
            throw new NotImplementedException();
        }
    }
}

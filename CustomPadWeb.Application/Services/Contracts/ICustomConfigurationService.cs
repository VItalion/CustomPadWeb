using CustomPadWeb.Common.ViewModels;

namespace CustomPadWeb.Application.Services.Contracts
{
    public interface ICustomConfigurationService
    {
        Task Create(CustomPadViewModelBase vm);
        Task Update(Guid id, UpdatePadViewModel vm);
    }
}

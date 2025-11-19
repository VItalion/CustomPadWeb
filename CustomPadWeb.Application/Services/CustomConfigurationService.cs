using CustomPadWeb.Application.Services.Contracts;
using CustomPadWeb.Common.ViewModels;
using CustomPadWeb.Domain.Entities;
using CustomPadWeb.Domain.Enums.PadOptions;
using CustomPadWeb.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomPadWeb.Application.Services
{
    public class CustomConfigurationService : ICustomConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomConfigurationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CustomPadViewModelBase vm)
        {
            var model = new GamepadConfiguration
            {
                Name = vm.Name,
                ABXYButtons = vm.ABXYButtons.MapTo<ButtonType>(),
                AdditionalButtons = vm.AdditionalButtons.MapTo<ButtonType>(),
                ConnectionType = vm.ConnectionType.MapTo<ConnectionType>(),
                Description = vm.Description,
                DPad = vm.DPad.MapTo<DPadType>(),
                InputType = vm.InputType.MapTo<InputType>(),
                Power = vm.Power.MapTo<PowerOption>(),
                Sticks = vm.Sticks.MapTo<StickType>(),
                Triggers = vm.Triggers.MapTo<TriggerType>()
            };

            await _unitOfWork.GamepadConfigurations.AddAsync(model).ConfigureAwait(false);
        }

        public async Task Update(Guid id, UpdatePadViewModel vm)
        {
            var model = await _unitOfWork.GamepadConfigurations.GetByIdAsync(id).ConfigureAwait(false);
            if (model == null)
                throw new KeyNotFoundException("Gamepad configuration not found.");

            model.Name = vm.Name;
            model.ABXYButtons = vm.ABXYButtons.MapTo<ButtonType>();
            model.AdditionalButtons = vm.AdditionalButtons.MapTo<ButtonType>();
            model.ConnectionType = vm.ConnectionType.MapTo<ConnectionType>();
            model.Description = vm.Description;
            model.DPad = vm.DPad.MapTo<DPadType>();
            model.InputType = vm.InputType.MapTo<InputType>();
            model.Power = vm.Power.MapTo<PowerOption>();
            model.Sticks = vm.Sticks.MapTo<StickType>();
            model.Triggers = vm.Triggers.MapTo<TriggerType>();

            _unitOfWork.GamepadConfigurations.Update(model);
            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}

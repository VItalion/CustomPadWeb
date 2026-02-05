using CustomPadWeb.Application.Services.Contracts;
using CustomPadWeb.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CustomPadWeb.ApiService.Controllers
{
    public partial class ConfigurationController(ICustomConfigurationService service, ILogger<ConfigurationController> logger) : ControllerBase
    {
        [HttpGet("api/configuration/{id:guid}")]
        public async Task<IActionResult> GetConfigurationByIdAsync(Guid id)
        {
            LogInformation($"Received request to get configuration with ID: {id}");
            var configuration = await service.GetByIdAsync(id);
            if (configuration == null)
            {
                LogWarning($"Configuration with ID: {id} not found");
                return NotFound();
            }
            LogInformation($"Successfully retrieved configuration with ID: {id}");
           
            return Ok(configuration);
        }

        [HttpGet("api/configuration/")]
        public async Task<IActionResult> GetAllConfigurationsAsync(int range = 0, int skip = 0)
        {
            LogInformation($"Received request to get all configurations with range: {range} and skip: {skip}");
            var configurations = await service.GetAllAsync(range, skip);
            LogInformation($"Successfully retrieved {configurations.Count()} configurations");

            return Ok(configurations);
        }

        [HttpPost("api/configuration/")]
        public async Task<IActionResult> CreateConfigurationAsync([FromBody] CustomPadViewModel vm)
        {
            LogInformation($"Received request to create a new configuration");
            await service.CreateAsync(vm);
            LogInformation($"Successfully created a new configuration");

            return CreatedAtAction(nameof(GetConfigurationByIdAsync), new { id = vm.Id }, vm);
        }

        [HttpPut("api/configuration/")]
        public async Task<IActionResult> UpdateConfigurationAsync([FromQuery] Guid id, [FromBody] UpdatePadViewModel vm)
        {
            LogInformation($"Received request to update configuration with ID: {id}");
            await service.UpdateAsync(id, vm);
            LogInformation($"Successfully updated configuration with ID: {id}");
         
            return NoContent();
        }

        [HttpDelete("api/configuration/{id:guid}")]
        public async Task<IActionResult> DeleteConfigurationAsync(Guid id)
        {
            LogInformation($"Received request to delete configuration with ID: {id}");
            await service.DeleteAsync(id);
            LogInformation($"Successfully deleted configuration with ID: {id}");
         
            return NoContent();
        }

        [LoggerMessage(Level = LogLevel.Information, Message = "{message}")]
        private partial void LogInformation(string message);

        [LoggerMessage(Level = LogLevel.Warning, Message = "{message}")]
        private partial void LogWarning(string message);
    }
}

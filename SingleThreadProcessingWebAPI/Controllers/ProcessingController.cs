using Microsoft.AspNetCore.Mvc;
using SingleThreadProcessingWebAPI.Services;

namespace SingleThreadProcessingWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessingController : ControllerBase
    {
        [HttpPost("Connect")]
        public async Task<IActionResult> Connect()
        {
            await ContextOfProcessingService.ExecuteFunctionAsync(async () =>
            {
                // Llama al m�todo de la clase est�tica aqu�
                ContextOfProcessingService.SimulateConnection();
                await Task.Delay(100);
            });

            return Ok("Connected");
        }

        [HttpPost("StartDispensation")]
        public async Task<IActionResult> StartDispensation()
        {
            await ContextOfProcessingService.ExecuteFunctionAsync(async () =>
            {
                // Llama al m�todo de la clase est�tica aqu�
                ContextOfProcessingService.SimulateDispensation();
                await Task.Delay(100);
            });

            return Ok("Dispensed");
        }
    }
}

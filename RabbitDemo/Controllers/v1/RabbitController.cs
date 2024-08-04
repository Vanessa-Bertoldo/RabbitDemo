using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitDemo.Services;

namespace RabbitDemo.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class RabbitController : ControllerBase
    {
        private readonly RabbitMQService _rabbitMQService;

        public RabbitController(RabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        [HttpPost("create-queue")]
        public IActionResult CreateQueue([FromQuery] string queueName)
        {
            _rabbitMQService.CreateQueue(queueName);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPost("send-message")]
        public IActionResult SendMessage([FromQuery] string queueName, [FromBody] string message)
        {
            _rabbitMQService.SendMessage(queueName, message);
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
